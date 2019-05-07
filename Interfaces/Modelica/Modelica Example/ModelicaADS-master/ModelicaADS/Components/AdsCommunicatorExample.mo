within ModelicaADS.Components;
model AdsCommunicatorExample "Example model to show ADS-Communication"
  extends Internal.AdsCommunicatorBasic(
          final nin = nWrite,
          final nout = nRead,
          final samplePeriod = samplePeriodExample,
          final startTime=startTimeExample,
          final AmsNetId=AmsNetIdExample,
          final port=portExample); //Extends basic TCP communication model

  /**************** necessary Input ****************************/
  parameter Modelica.SIunits.Time samplePeriodExample = 1
    "Sample period how often a telegram is send";
  parameter Modelica.SIunits.Time startTimeExample = 0
    "Start time when sampling starts";

  parameter Integer  AmsNetIdExample[6] = {10,39,190,36,1,1}
    "AMS Net ID of PLC";
  parameter Integer portExample=851 "Port on PLC to connect to";
  parameter String varNameToWrite[nWrite] = {"MAIN.myInputVar1","MAIN.myInputVar2"}
    "Names of variables to be written in PLC";
  parameter String varNameToRead[nRead] = {"MAIN.myOutputVar1","MAIN.myOutputVar2"}
    "Names of variables to be read from PLC";
  parameter Integer nWrite = 2 "Number of datapoints to be written";
  parameter Integer nRead = 2 "Number of datapoints to be read";

  Real ReceiveData "Real data to be received";
  Integer stateExample
    "dummy variable to check state of function, 0 == OK, 1 == errror";

algorithm
  when {sampleTrigger} then

   for i in 1:nWrite loop
   // Send input to PLC
      stateExample := ModelicaADS.Functions.ADS.funAdsSendReal(u[i],
        varNameToWrite[i]);
   // For debugging only
   //Modelica.Utilities.Streams.print("Call function funAdsSendReal() successful");
   end for;
  for i in 1:nRead loop
  // Read output from PLC
      (ReceiveData,stateExample) :=
        ModelicaADS.Functions.ADS.funAdsReceiveReal(varNameToRead[i]);
  // For debugging only
  //Modelica.Utilities.Streams.print("3 - Call function funAdsReceiveReal():");
  // Connect to output connector
  y[i] := ReceiveData;
  end for;
end when;

annotation (
Documentation(info="<html>
<h4>How to connect to TwinCAT PLC</h4>
<p>To connect to a TwinCAT PLC it is necessary to create the route to the remote PLC first. Open a project in TwinCAT 3, and add route be searching for the IP of the remote PLC.</p>
<h4>System Requirements</h4>
<p>Code tested with Windows 7 64bit, Dymola 2015 32bit, TwinCAT 3.1 Build 4018 </p>
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
</html>"),Icon(coordinateSystem(preserveAspectRatio=false, extent={{-100,-100},
            {100,100}}), graphics={
        Rectangle(
          extent={{-100,100},{100,-100}},
          lineColor={0,0,255},
          fillColor={255,255,255},
          fillPattern=FillPattern.Solid),
        Rectangle(
          extent={{-88,66},{-30,30}},
          lineColor={0,0,255},
          fillColor={255,255,255},
          fillPattern=FillPattern.Solid),
        Rectangle(
          extent={{26,66},{84,30}},
          lineColor={0,0,255},
          fillColor={255,255,255},
          fillPattern=FillPattern.Solid),
        Rectangle(
          extent={{-88,22},{-30,10}},
          lineColor={0,0,255},
          fillColor={255,255,255},
          fillPattern=FillPattern.Solid),
        Rectangle(
          extent={{26,22},{84,10}},
          lineColor={0,0,255},
          fillColor={255,255,255},
          fillPattern=FillPattern.Solid),
        Text(
          extent={{-38,10},{28,-36}},
          lineColor={0,0,0},
          fillColor={255,255,255},
          fillPattern=FillPattern.Solid,
          textString="ADS"),
        Polygon(
          points={{-50,-10},{-96,-46},{-50,-80},{-50,-10}},
          smooth=Smooth.None,
          fillColor={0,0,0},
          fillPattern=FillPattern.Solid,
          pattern=LinePattern.None),
        Rectangle(
          extent={{-50,-38},{40,-52}},
          fillColor={0,0,0},
          fillPattern=FillPattern.Solid,
          pattern=LinePattern.None),
        Rectangle(
          extent={{-86,64},{-32,32}},
          lineColor={0,0,255},
          fillColor={255,255,255},
          fillPattern=FillPattern.Solid),
        Rectangle(
          extent={{28,64},{82,32}},
          lineColor={0,0,255},
          fillColor={255,255,255},
          fillPattern=FillPattern.Solid),
        Polygon(
          points={{40,-10},{86,-42},{40,-80},{40,-10}},
          smooth=Smooth.None,
          fillColor={0,0,0},
          fillPattern=FillPattern.Solid,
          pattern=LinePattern.None)}));
end AdsCommunicatorExample;
