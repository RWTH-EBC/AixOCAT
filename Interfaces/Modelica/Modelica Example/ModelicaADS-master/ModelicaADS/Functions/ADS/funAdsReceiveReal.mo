within ModelicaADS.Functions.ADS;
function funAdsReceiveReal
  "External C function to receive real number on current socket"
  input String varName "Name of variable to be read from PLC";
output Real recvData "buffer where incoming message is saved";
output Integer state
    "Error handling: 0 = OK!, 1 == Error, see simulation tab for info";

external "C" state=funAdsReceiveReal(recvData,varName)
annotation (Include="#include \"AdsHeaderFile.h\"",
             Library="TcAdsDll",
             IncludeDirectory="modelica://ModelicaADS/Resources/Include",
             LibraryDirectory="modelica://ModelicaADS/Resources/Library");

annotation (Documentation(info="<html>

<p>This is a function to receive a Real value from a Beckhoff PLC via the ADS
protocol. The functions utilised from the C API provided by Beckhoff are
<code>AdsSyncReadWriteReq(), AdsSyncReadReq()</code> and <code>AdsSyncWriteReq()</code>.
Errors are reported in the simulation tab.</p>
<p>The basic functioning is:<br />
Step 1: Get the variable handle of the variable to be written on in the PLC;<br />
Step 2: Read the value of <code>myOutputVar</code> from in the PLC;<br />
Step 3: Release handle of the variable in the PLC<br /></p>
<h4>!!! Important !!!</h4>
<p>Up to now the variable name of the input variable in the PLC is HARDCODED!
This means the variable name in the PLC
need to be exactly <code>myOutputVar<code>.</p>
<h4>C Source Code of Function</h4>
<pre>
// Receive double
int funAdsReceiveReal(double *p_RecvData)
{
        long        nErr; // Variable for error handling
        ULONG        lHdlVar; // lHdlVar
        char      szVar []={\"MAIN.myOutputVar\"}; // Specify variable which should be written
        unsigned long szVarLen = sizeof(szVar);

        // Step 1: Get variable handle
        nErr = AdsSyncReadWriteReq(pAddr,
                                                           ADSIGRP_SYM_HNDBYNAME,
                                                           0x0, 
                                                           sizeof(lHdlVar), 
                                                           &lHdlVar, 
                                                           szVarLen, 
                                                           szVar);
        if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Function to fetch handle called!\\n\");
        if (nErr != 0)
                {
                ModelicaFormatMessage(\"Error: Function to fetch handle failed: %i\\n\",nErr);
                return(1);
                }
        if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Function call to fetch handle sucessful!\\n\");
        
        // Read the recvData from the PLC
                nErr = AdsSyncReadReq(pAddr, 
                                                          ADSIGRP_SYM_VALBYHND,                // IndexGroup 
                                                                lHdlVar,                                // IndexOffset
                                                                sizeof(*p_RecvData),        // Size of data to received
                                                                p_RecvData);                        // Data to be received
        
        if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Function to receive data called!\\n\");
        if (nErr != 0)
                {
                ModelicaFormatMessage(\"Error: Function to receive data failed with code: %i\\n\",nErr);
                return(1);
                }
        if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Function to receive data sucessful!\\n\");
        //Release handle of plc variable
        nErr = AdsSyncWriteReq(pAddr, ADSIGRP_SYM_RELEASEHND, 0, sizeof(lHdlVar), &lHdlVar); 
        if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Function to release handle called!\\n\");
        if (nErr != 0)
                {
                ModelicaFormatMessage(\"Error: Function to release handle failed with code: %i\\n\",nErr);
                return(1);
                }
        if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Function to release handle sucessful!\\n\");

        if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Function to receive/right by handle in total successful! the data read is:\\n\");
        if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"The read data is %f\\n\",*p_RecvData);
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
end funAdsReceiveReal;
