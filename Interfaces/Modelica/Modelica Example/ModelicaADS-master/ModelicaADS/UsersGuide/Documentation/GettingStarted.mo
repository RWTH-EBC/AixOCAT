within ModelicaADS.UsersGuide.Documentation;
class GettingStarted "Getting started"
  extends Modelica.Icons.Information;

  annotation (Documentation(info="<html>
<p>This package contains functions to exchange information between a Beckhoff PLC and a Modelica simulation model at runtime. The package uses functions provided through a C API provided for use by Beckhoff via *.dll-file and two header files by Beckhoff. </p>
<p>The user needs to provide the AMD net ID of the target PLC and the port of the machine, usually this is 851. Also the names of the in and output variables as they are named in the PLC need to be provided. </p>
<h4>General Communication Procedure</h4>
<p>Currently only functions to read and write a floating point number are provided. The communication is structured as follows: </p>
<p>1.) Before starting the communication call funAdsConstrutor() open an ADS port on the local system and connect to the foreign system;</p>
<p>2.) Send and receive with a fixed samplingTime values at runtime via funAdsSendReal() and funAdsReceiveReal();</p>
<p>3.) End communcation when simulation finished by calling funAdsDestructor().</p>

<h4>Starting Point</h4>
<p>A good starting point is the model <a href=\"modelica://ModelicaADS.Examples.ExampleAdsLoop\">ExampleAdsLoop</a> which serves as an example how to handle the ADS communication within a simulation setting</p>

<p>Error codes from the ADS C API are reported via ModelicaFormatMessage() and displayed in the simulation tab. </p>
</html>",
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
end GettingStarted;
