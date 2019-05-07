within ModelicaADS.UsersGuide.Documentation;
class Overview "Overview"
   extends Modelica.Icons.Information;

   annotation (preferredView=Info, Documentation(info="<HTML>

<h4>Introduction</h4>
   <p>This is a simple package to connect Dymola/Modelica to a Beckhoff PLC via 
   the proprietary C API and the ADS protocol provided by Beckhoff (c).</p>
   <p> The functions used to perform the communication are derived from the functions
   documented by Beckhoff. A complete documentation and additional functions are provided here: 
   <a href=\"http://infosys.beckhoff.de/index.php?content=../content/1031/tc3_adssamples_c/html/tcadsdll_api_cpp_setup_tc3.htm&id=17123\">Beckhoff infosys documentation</a>
   </p>
   <h4>Requirements and Prerequisities</h4>
      
   <p>Functionalities tested with Dymola 2015 32-bit, Microsoft Windows 7
   64-bit and Microsoft Visual Studio 2010 Professional as a compiler.</p>
  <p>Please check on your local installation the path variables within <code>AdsHeaderFile.h</code> if the point the right path to <code>TcAdsDef.h</code> and <code>TcAdsAPI.h</code>.</p>
   
   <p>NOTE Dymola in 64-bit mode does not work!</p>
</HTML>",
revisions="<html>
<ul>
  <li><i>November 01, 2015&nbsp;</i>
         by Georg Ferdinand Schneider:<br>
         Implemented</li>
  <li><i>January 30, 2016&nbsp;</i>
         by Georg Ferdinand Schneider:<br>
         Revised for publishing</li>
</ul>
</html>"));
end Overview;
