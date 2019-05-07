within ModelicaADS.Functions.ADS;
function funAdsSendReal
  "External C function to send Real variable via current socket"

input Real sendData "Data to be send as a real";
input String varName "Variable Name in PLC";

output Integer ans
    "Error handling: 0 = OK!, 1 == Error, see simulation tab for info";

external "C" ans = funAdsSendReal(sendData,varName)
 annotation (Include="#include \"AdsHeaderFile.h\"",
             Library="TcAdsDll",
             IncludeDirectory="modelica://ModelicaADS/Resources/Include",
             LibraryDirectory="modelica://ModelicaADS/Resources/Library");

annotation (Documentation(info="<html>
<p>This is a function to send a Real value to a Beckhoff PLC via the ADS
protocol. The functions utilised from the C API provided by Beckhoff are
<code>AdsSyncReadWriteReq()</code> and <code>AdsSyncWriteReq()</code>.
Errors are reported in the simulation tab.</p>
<p>The basic functioning is:<br />
Step 1: Get the variable handle of the variable to be written on in the PLC;<br />
Step 2: Write the value of <code>sendData</code> to the variable in the PLC;<br />
Step 3: Release handle of the variable in the PLC<br /></p>
<h4>!!! Important !!!</h4>
<p>Up to now the variable name of the input variable in the PLC is HARDCODED!
This means the variable name in the PLC
need to be exactly <code>myInputVar<code>.</p>
<h4>C Source Code of Function</h4>
<pre>
int funAdsSendReal(double sendData)
{
long        nErr; // Variable for error handling
ULONG        lHdlVar; // Variable to save handle
double *p_SendData = &sendData; // Pointer to variable which contains data to send
                
// HARDCODED variable name in PLC
char      szVar []={\"MAIN.myInputVar\"}; // Specify variable which should be written
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
if (nErr != 0){
ModelicaFormatMessage(\"Error: Function to fetch handle failed: %i\\n\",nErr);
return(1);}
  if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Function call to fetch handle sucessful!\\n\");
        
// Write the sendData to the PLC
nErr = AdsSyncWriteReq(pAddr, 
                       ADSIGRP_SYM_VALBYHND,// IndexGroup 
                       lHdlVar,// IndexOffset
                       sizeof(sendData),// Size of data to send
                       p_SendData);// Data to be send
        
  if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Function to send data called!\\n\");
if (nErr != 0){
  ModelicaFormatMessage(\"Error: Function to send data failed with code: %i\\n\",nErr);
  return(1);}
  
  if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Function to send data sucessful!\\n\");
  
//Release handle of PLC variable
nErr = AdsSyncWriteReq(pAddr,
                       ADSIGRP_SYM_RELEASEHND,
                       0,
                       sizeof(lHdlVar),
                       &lHdlVar); 
  if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Function to release handle called!\\n\");
if (nErr != 0){
ModelicaFormatMessage(\"Error: Function to release handle failed with code: %i\\n\",nErr);
return(1);}

  if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Function to release handle sucessful!\\n\");
  if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Function to write by handle in total successful! the data send is:\\n\");
                ModelicaFormatMessage(\"The send data is %f\\n\",sendData);
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
end funAdsSendReal;
