within ModelicaADS.Functions.ADS;
function funAdsConstructor
  "External C function to construct a socket for ADS communication"
  input Integer port "Port on Beckhoff CPU to connect to e.g. 351 or 851";
   input Integer AmsNetID1
    "First digit of AMS Net ID i.e. 192.---.---.---.---.---";
   input Integer AmsNetID2
    "First digit of AMS Net ID i.e. ---.168.---.---.---.---";
   input Integer AmsNetID3
    "First digit of AMS Net ID i.e. ---.---.0.---.---.---";
   input Integer AmsNetID4
    "First digit of AMS Net ID i.e. ---.---.---.1.---.---";
   input Integer AmsNetID5
    "First digit of AMS Net ID i.e. ---.---.---.---.1.---";
   input Integer AmsNetID6
    "First digit of AMS Net ID i.e. ---.---.---.---.---.1";

output Integer ans
    "Error handling: 0 = OK!, 1 == Error, see simulation tab for info";

external "C" ans = funAdsConstructor(port,
                                     AmsNetID1,
                                     AmsNetID2,
                                     AmsNetID3,
                                     AmsNetID4,
                                     AmsNetID5,
                                     AmsNetID6)
                  annotation(Include="#include \"AdsHeaderFile.h\"",
                             Library="TcAdsDll",
                             IncludeDirectory=
        "modelica://ModelicaADS/Resources/Include",
                             LibraryDirectory=
        "modelica://ModelicaADS/Resources/Library");
annotation (Documentation(info="<html>
<p>This is a function to establish the connection to a Beckhoff PLC via the ADS
protocol. The function utilised from the C API provided by Beckhoff is
<code>AdsPortOpen()</code>. No errors are reported.</p>
<h4>C Source Code of Function</h4>
<pre>
// Establish ADS connection
int funAdsConstructor(int portNumber,
                      int AmsNetID1,
                      int AmsNetID2,
                      int AmsNetID3,
                      int AmsNetID4,
                      int AmsNetID5,
                      int AmsNetID6)
{
long nPort;
  if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Call AdsPortOpen()!\\n\");
// ADS COMMUNICATION: Open communication port on the ADS router
nPort = AdsPortOpen();
  if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Call AdsPortOpen() sucessful! Port used is: %i\\n\",nPort);
  			
// Set port number and AMS net ID:
pAddr->port = portNumber;
(*pAddr).netId.b[0] = AmsNetID1;
(*pAddr).netId.b[1] = AmsNetID2;
(*pAddr).netId.b[2] = AmsNetID3;
(*pAddr).netId.b[3] = AmsNetID4;
(*pAddr).netId.b[4] = AmsNetID5;
(*pAddr).netId.b[5] = AmsNetID6;
	 
  if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Call of funAdsConstructor() Sucessful!\\n\");
return(0);
}
</pre>

</html>", revisions="<html>
<ul>
  <li><i>November 01, 2015&nbsp;</i>
         by Georg Ferdinand Schneider:<br>
         Implemented</li>
  <li><i>January 30, 2016&nbsp;</i>
         by Georg Ferdinand Schneider:<br>
         Revised for publishing</li>
</ul>
</html>"));
end funAdsConstructor;
