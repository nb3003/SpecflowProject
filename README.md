# SpecflowProject
This contains 3 projects built with C# and Specflow 3.0 

### Core.Specflow 
.NET Standard Project which contains StepArgumentTransformation in Specflow. This will convert string to DateTimeOffset

### SpecflowTestCore 
.NET Core 2.2 project which contains the NUNIT Tests for Step Argument Transformers in Core.Specflow project. 
* Core.Specflow is added as External Assembly
* Specflow 3.0.225, Specflow.NUnit 3.0.225, Specflow.Tools.MsBuildGeneration 3.0.225, NUnit 3.11.0, NUnit3TestAdapter 3.11.0 packages are used
* specflow.json is used to read specflow configuration
* There are 2 tests written here, which fail because the StepArgumentTransformation is not working in this project. 

### SpecflowTestFramework
.NET Framework 4.7.2 project which contains the NUNIT Tests for Step Argument Transformers in Core.Specflow project. 
* Core.Specflow is added as External Assembly
* Specflow 3.0.225, Specflow.NUnit 3.0.225, Specflow.Tools.MsBuildGeneration 3.0.225, NUnit 3.11.0 packages are used
* App.config is used to read specflow configuration
* All 2 tests written in SpecflowFeature1.feature pass. It returns DateTimeOffset.


Visual Studio 2019 - Version 16.2.2
Specflow Extension version installed in VS2019 - 2019.0.31.31805
