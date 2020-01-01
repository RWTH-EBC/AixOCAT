within ModelicaADS.Functions.ADS;
function funAdsDestructor "External C function to close socket and free memory"
output Integer ans
    "Error handling: 0 = OK!, 1 == Error, see simulation tab for info";

external "C" ans = funAdsDestructor()
    annotation(Include="#include \"AdsHeaderFile.h\"",
               Library="TcAdsDll",
               IncludeDirectory="modelica://ModelicaADS/Resources/Include",
               LibraryDirectory="modelica://ModelicaADS/Resources/Library");

annotation (Documentation(info="<html>
<p>This is a function to close the connection with a Beckhoff PLC via the ADS
protocol. The function utilised from the C API provided by Beckhoff is
<code>AdsPortClose()</code>. Errors are reported.</p>
<h4>C Source Code of Function</h4>
<pre>
int funAdsDestructor(void)
{
	long nErr; // Variable for error codes from ADS 
	
	// Close communication port
	nErr = AdsPortClose();
			if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"Call of AdsPortClose(), done!\\n\");
  	if (nErr != 0)
		{// Error checking
		ModelicaFormatMessage(\"Error: AdsPortClose(): %i\\n\",nErr);
		return(1);
		}
	if (DEBUG_FLAG != 0) ModelicaFormatMessage(\"AdsPortClosed successful!\\n\");
	
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
end funAdsDestructor;
