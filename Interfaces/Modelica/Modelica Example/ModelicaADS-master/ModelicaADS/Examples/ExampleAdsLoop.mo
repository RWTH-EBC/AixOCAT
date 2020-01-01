within ModelicaADS.Examples;
model ExampleAdsLoop "Test Ads communication"
extends Modelica.Icons.Example;
  Modelica.Blocks.Continuous.FirstOrder system(
    k=1,
    T=1,
    initType=Modelica.Blocks.Types.Init.InitialOutput,
    y_start=5) "Simple first order system to be controlled"
               annotation (Placement(transformation(extent={{-26,28},{-6,48}})));
  Modelica.Blocks.Math.Feedback feedback annotation (Placement(transformation(
        extent={{-10,-10},{10,10}},
        rotation=180,
        origin={0,-10})));
  Modelica.Blocks.Nonlinear.Limiter limiter(uMax=100, uMin=-100)
    "limits output of controller"                                annotation (
      Placement(transformation(
        extent={{-10,-10},{10,10}},
        rotation=180,
        origin={-64,-10})));
  Modelica.Blocks.Math.Gain gain(k=10) "Only gain controller"
                                       annotation (Placement(transformation(
        extent={{-10,-10},{10,10}},
        rotation=180,
        origin={-28,-10})));
  Modelica.Blocks.Sources.Pulse pulse(
    amplitude=5,
    period=1,
    offset=5) "Pulse of set point"
              annotation (Placement(transformation(
        extent={{-10,-10},{10,10}},
        rotation=180,
        origin={30,-10})));
  Components.AdsCommunicatorExample adsCommunicatorExample(
    nWrite=1,
    nRead=1,
    varNameToWrite={"MAIN.myInputVar"},
    varNameToRead={"MAIN.myOutputVar"})
    "This model will exchange every sample period a telegram to the PLC"
    annotation (Placement(transformation(extent={{-66,30},{-46,50}})));
equation

  connect(system.y, feedback.u2)  annotation (Line(
      points={{-5,38},{0,38},{0,-2},{8.88178e-016,-2}},
      color={0,0,127},
      smooth=Smooth.None));
  connect(gain.u, feedback.y) annotation (Line(
      points={{-16,-10},{-9,-10}},
      color={0,0,127},
      smooth=Smooth.None));
  connect(gain.y, limiter.u) annotation (Line(
      points={{-39,-10},{-52,-10}},
      color={0,0,127},
      smooth=Smooth.None));
  connect(pulse.y, feedback.u1) annotation (Line(
      points={{19,-10},{8,-10}},
      color={0,0,127},
      smooth=Smooth.None));
  connect(adsCommunicatorExample.y[1], system.u) annotation (Line(
      points={{-45,40},{-36,40},{-36,38},{-28,38}},
      color={0,0,127},
      smooth=Smooth.None));
  connect(adsCommunicatorExample.u[1], limiter.y) annotation (Line(
      points={{-68,40},{-84,40},{-84,-10},{-75,-10}},
      color={0,0,127},
      smooth=Smooth.None));
  annotation (Diagram(coordinateSystem(preserveAspectRatio=false, extent={{-100,
            -100},{100,100}}), graphics),
Documentation(revisions="<html>
<ul>
  <li><i>November 01, 2015&nbsp;</i>
         by Georg Ferdinand Schneider:<br>
         Implemented</li>
  <li><i>January 30, 2016&nbsp;</i>
         by Georg Ferdinand Schneider:<br>
         Revised for publishing</li>
</ul>
</html>",information="<html>

<p>This is a very simple example to show ADS-Communication functionality. A feedback
 control is modeled where a gain controller controls a first order block. The signal
 is send to a PLC which multiplies the value by two and returns it.</p>
 <p>The target PLC needs to be running to be accessible.</p>
</html>",
      info="<html>
<p>Please see Documentation package in the UsersGuide for explanation.</p>
</html>"),
 experiment(StopTime=10, __Dymola_Algorithm="Lsodar"),
    __Dymola_experimentSetupOutput);
end ExampleAdsLoop;
