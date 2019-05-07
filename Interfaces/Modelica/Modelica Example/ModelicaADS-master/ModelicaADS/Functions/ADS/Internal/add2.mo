within ModelicaADS.Functions.ADS.Internal;
function add2 "Dummy function just for checking compilation functioning."
  input Real A,B "Two inputs";
  output Real C "One output";
  output Integer state "Other output";

  external "C" state = add2(A,B,C) annotation (
      Include="#include \"AdsHeaderFile.h\"",
      Library="TcAdsDll",
      IncludeDirectory="modelica://ModelicaADS/Resources/Include",
      LibraryDirectory="modelica://ModelicaADS/Resources/Library");

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
end add2;
