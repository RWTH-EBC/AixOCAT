within ModelicaADS.Components.Internal;
partial model AdsCommunicatorBasic
  "Partial Model of ADS-Interface, minimum code needs additional information"

extends Modelica.Blocks.Interfaces.DiscreteMIMO; // Base Class Discrete MIMO for discrete events i.e. sampleTrigger

/**************** necessary Input ****************************/
  parameter Integer  AmsNetId[6] = {10,39,190,36,1,1}
    "AMS Net ID of target PLC";
  parameter Integer  port = 851 "Port on server";

/**************** Error handling of functions ***********************/
   Integer state
    "dummy variable to check state of C-function, 0 == OK, 1 == failure";

initial algorithm
  /**************** initialize ADS communication **************/
  state := ModelicaADS.Functions.ADS.funAdsConstructor(
    port,
    AmsNetId[1],
    AmsNetId[2],
    AmsNetId[3],
    AmsNetId[4],
    AmsNetId[5],
    AmsNetId[6]);
  Modelica.Utilities.Streams.print("funAdsConstructor() successful!");

equation

algorithm

 when terminal() then
/**************** Terminate communication  **************/
    state := ModelicaADS.Functions.ADS.funAdsDestructor();
    Modelica.Utilities.Streams.print("funAdsDestructor() successful");
 end when;

annotation (
Diagram(coordinateSystem(preserveAspectRatio=false,
       extent={{-100,-100},{100,100}}),graphics),
Documentation(info="<html>
<p>This is the partial model of a communicator model in a simulation.</p>
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
</html>"),
    Icon(coordinateSystem(preserveAspectRatio=false, extent={{-100,-100},{100,100}}),
        graphics));
end AdsCommunicatorBasic;
