Imports TwinCAT
Imports TwinCAT.Ads
Imports TwinCAT.Ads.TypeSystem
Imports TwinCAT.TypeSystem

Public Module ADS

#Region "Properties"
    Dim client As New TcAdsClient
    Dim SymbolLoader As ISymbolLoader
    Dim SymbolCache As Dictionary(Of String, Symbol)
    Dim ValueCache As Dictionary(Of String, Decimal)
    Dim TimeCache As Dictionary(Of String, Date)
    Public netID As String
    Public port As Integer 'Standard Port851
    Private _address As AmsAddress
    Private _cachedOnly As Boolean
    Private _connected As Boolean
    Private _initialized As Boolean
    Private _timeout As Boolean
#End Region

#Region "Property Functions"
    Public Property CachedOnly As Boolean
        Get
            CachedOnly = _cachedOnly
        End Get
        Set(ByVal Value As Boolean)
            If _cachedOnly <> Value Then
                _cachedOnly = Value
                If _cachedOnly Then
                    Log.AddMessage(MessageType.Information, "Switching to cached only mode")
                Else
                    Log.AddMessage(MessageType.Information, "Switching to live data mode")
                End If
            End If
        End Set
    End Property

    Public ReadOnly Property Connected As Boolean
        Get
            Connected = _connected
        End Get
    End Property

    Public ReadOnly Property Timeout As Boolean
        Get
            Timeout = _timeout
        End Get
    End Property

    Public ReadOnly Property Initialized As Boolean
        Get
            Initialized = _initialized
        End Get
    End Property
#End Region

#Region "Public Functions"
    Public Sub connect(ByVal _netID As String, ByVal _port As Integer)
        netID = _netID
        port = _port
        _address = New AmsAddress(netID & ":" & port)
        _timeout = False
        Dim settings As SymbolLoaderSettings = New SymbolLoaderSettings(SymbolsLoadMode.Flat)
        'connect to the ads server
        client.Synchronize = False
        client.Connect(_address)
        'create list of symbols and initialize cache
        SymbolLoader = SymbolLoaderFactory.Create(client, settings)
        SymbolCache = New Dictionary(Of String, Symbol)
        ValueCache = New Dictionary(Of String, Decimal)
        TimeCache = New Dictionary(Of String, Date)
        _connected = IsConnected()
        _initialized = True
    End Sub

    Public Function CheckConnection() As Boolean
        Return IsConnected()
    End Function

    Private Function IsConnected() As Boolean
        Dim _state As Boolean
        If client.IsConnected And Not _timeout Then
            Try
                _state = (client.ReadState().AdsState = AdsState.Run)
            Catch ex As AdsErrorException
                If ex.ErrorCode = AdsErrorCode.ClientSyncTimeOut Then
                    Log.AddMessage(MessageType.Information, "Connection to PLC timed out. Aborting automatic reconnect.")
                    _timeout = True
                End If
                _state = False
            End Try
        Else
            _state = False
        End If
        If _connected Then
            _timeout = False
        End If
        If _state <> _connected Or Not _initialized Then
            _connected = _state
            If _connected Then
                Log.AddMessage(MessageType.Information, "Connection to PLC successful")
            Else
                Log.AddMessage(MessageType.MajorError, "Connection to PLC failed")
            End If
        End If
        Return _connected
    End Function

    Public Function getSymbols() As List(Of ISymbol)
        Dim symbols As List(Of ISymbol) = New List(Of ISymbol)
        For Each symbol As ISymbol In SymbolLoader.Symbols
            Dim subSymbols As List(Of ISymbol) = getSymbolsRecursive(symbol)
            For Each subSymbolRec As ISymbol In subSymbols
                symbols.Add(subSymbolRec)
            Next
        Next
        Return symbols
    End Function

    Public Function getSymbolValue(ByVal Symbolname As String)
        If Connected And Symbolname <> "" Then
            Try
                Dim _symbol As Symbol = GetSymbol(Symbolname)
                Return _symbol.ReadValue()
            Catch ex As Exception
                If IsConnected() Then
                    Debug.WriteLine(Symbolname & ": " & ex.ToString())
                    Log.AddMessage(MessageType.LightError, Symbolname & ": Error when reading value")
                End If
                Return (0)
            End Try
        Else
            Return (0)
        End If
    End Function

    Public Function getSymbolValueCached(ByVal Symbolname As String, ByVal CacheTime As Integer)
        If Symbolname Is Nothing Or Symbolname = "" Then
            Log.AddMessage(MessageType.LightError, "Empty Symbolname requested.")
            Return (0)
        End If
        If Not ValueCache.ContainsKey(Symbolname) Then
            ValueCache.Add(Symbolname, CDec(getSymbolValue(Symbolname)))
        End If
        If Not TimeCache.ContainsKey(Symbolname) Then
            TimeCache.Add(Symbolname, Date.UtcNow)
        End If
        If Date.UtcNow > TimeCache(Symbolname).AddMilliseconds(CacheTime) And Not CachedOnly Then
            ValueCache(Symbolname) = CDec(getSymbolValue(Symbolname))
            TimeCache(Symbolname) = Date.UtcNow
        End If
        Return ValueCache(Symbolname)
    End Function

    Public Function getSymboleType(ByVal Symbolname As String) As String
        If client.IsConnected And Symbolname <> "" Then
            Return SymbolLoader.Symbols.Item(Symbolname).TypeName
        End If
        Return ("")
    End Function
#End Region

#Region "Public Subs"
    Public Sub setSymboleValue(ByVal Symbolname As String, ByVal Value As Object)
        If client.IsConnected And Symbolname <> "" Then
            Try
                Dim _symbol As Symbol = GetSymbol(Symbolname)
                _symbol.WriteValue(Value)
                If ValueCache.ContainsKey(Symbolname) Then
                    ValueCache(Symbolname) = CDec(Value)
                End If
            Catch ex As Exception
                If IsConnected() Then
                    Debug.WriteLine(Symbolname & ":" & ex.ToString())
                    Log.AddMessage(MessageType.LightError, Symbolname & ": Error when writing value")
                End If
            End Try
        End If
    End Sub

    Public Sub DisposeADS()
        client.Dispose()
    End Sub
#End Region

#Region "Private Functions"
    Private Function getSymbolsRecursive(ByVal symbol As ISymbol) As List(Of ISymbol)
        Dim symbols As List(Of ISymbol) = New List(Of ISymbol)
        symbols.Add(symbol)
        'Dim subSymbolProvider As ITcAdsSymbolBrowser = symbol
        If symbol.SubSymbols.Count > 0 Then
            For Each subSymbol As ISymbol In symbol.SubSymbols
                Dim subSymbols As List(Of ISymbol) = getSymbolsRecursive(subSymbol)
                For Each subSymbolRec As ISymbol In subSymbols
                    symbols.Add(subSymbolRec)
                Next
            Next
        End If
        Return symbols
    End Function

    Private Function GetSymbol(ByVal symbolPath As String) As Symbol
        Dim symbol As Symbol
        If SymbolCache.ContainsKey(symbolPath) Then
            symbol = SymbolCache(symbolPath)
        Else
            symbol = searchSymbolInTree(symbolPath)
            SymbolCache.Add(symbolPath, symbol)
        End If
        Return symbol
    End Function

    Private Function searchSymbolInTree(ByVal symbolPath As String) As ISymbol
        Dim PathSegments As String()
        Dim tempPath As String = ""
        Dim symbol As ISymbol
        PathSegments = symbolPath.Split(".")
        If PathSegments.Count > 0 Then
            tempPath = PathSegments(0)
            For i = 1 To UBound(PathSegments)
                tempPath = tempPath & "." & PathSegments(i)
                If i = 1 Then
                    symbol = SymbolLoader.Symbols(tempPath)
                Else
                    If symbol.SubSymbols.Count > 0 Then
                        symbol = symbol.SubSymbols.Item(PathSegments(i))
                    End If
                End If
            Next i
        End If
        Return symbol
    End Function
#End Region

End Module
