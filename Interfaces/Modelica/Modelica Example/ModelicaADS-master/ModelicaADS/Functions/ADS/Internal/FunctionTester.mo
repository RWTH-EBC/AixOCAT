within ModelicaADS.Functions.ADS.Internal;
model FunctionTester "Model to test functions with arbitrary values"
extends Modelica.Icons.Example;
  Integer state;
  Real ReceiveData;
algorithm

  Modelica.Utilities.Streams.print("Model to test functions:");
  Modelica.Utilities.Streams.print("1 - Call function funAdsConstructor():");
  state := ModelicaADS.Functions.ADS.funAdsConstructor(
    851,
    169,
    254,
    202,
    55,
    1,
    1);
  Modelica.Utilities.Streams.print("Successful!");

  Modelica.Utilities.Streams.print("2 - Call function funAdsSendReal():");
  state := ModelicaADS.Functions.ADS.funAdsSendReal(11.1, "MAIN.myInputVar");
  Modelica.Utilities.Streams.print("Successful!");

  Modelica.Utilities.Streams.print("3 - Call function funAdsReceiveReal():");
  (ReceiveData,state) := ModelicaADS.Functions.ADS.funAdsReceiveReal("MAIN.myOutputVar");
  Modelica.Utilities.Streams.print("Successful!");

  Modelica.Utilities.Streams.print("4 - Call function funAdsDestructor():");
  state := ModelicaADS.Functions.ADS.funAdsDestructor();
  Modelica.Utilities.Streams.print("Successful!");

  annotation (Documentation(revisions="<html>
<ul>
  <li><i>November 01, 2015&nbsp;</i>
         by Georg Ferdinand Schneider:<br>
         Implemented</li>
  <li><i>January 30, 2016&nbsp;</i>
         by Georg Ferdinand Schneider:<br>
         Revised for publishing</li>
</ul>
</html>"));
end FunctionTester;
