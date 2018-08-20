﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.2.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Youscan.POS.IntegrationTests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("POS Terminal")]
    public partial class POSTerminalFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "POS.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "POS Terminal", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Scan existing products")]
        [NUnit.Framework.TestCaseAttribute("ABCDABA", "13.25", null)]
        [NUnit.Framework.TestCaseAttribute("CCCCCCC", "6", null)]
        [NUnit.Framework.TestCaseAttribute("ABCD", "7.25", null)]
        public virtual void ScanExistingProducts(string scannedProducts, string totalPrice, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Scan existing products", exampleTags);
#line 3
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "ProductName",
                        "SinglePrice",
                        "VolumeCount",
                        "VolumePrice"});
            table1.AddRow(new string[] {
                        "A",
                        "1.25",
                        "3",
                        "3"});
            table1.AddRow(new string[] {
                        "B",
                        "4.25",
                        "",
                        ""});
            table1.AddRow(new string[] {
                        "C",
                        "1",
                        "6",
                        "5"});
            table1.AddRow(new string[] {
                        "D",
                        "0.75",
                        "",
                        ""});
#line 4
 testRunner.Given("I have products:", ((string)(null)), table1, "Given ");
#line 10
 testRunner.When(string.Format("I scan products {0}", scannedProducts), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
 testRunner.Then(string.Format("Total price will be {0}", totalPrice), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Scan non-existing products")]
        [NUnit.Framework.TestCaseAttribute("ABFR", "F", null)]
        [NUnit.Framework.TestCaseAttribute("G", "G", null)]
        [NUnit.Framework.TestCaseAttribute("HGK", "H", null)]
        public virtual void ScanNon_ExistingProducts(string scannedProducts, string notFound, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Scan non-existing products", exampleTags);
#line 20
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "ProductName",
                        "SinglePrice",
                        "VolumeCount",
                        "VolumePrice"});
            table2.AddRow(new string[] {
                        "A",
                        "1.25",
                        "3",
                        "3"});
            table2.AddRow(new string[] {
                        "B",
                        "4.25",
                        "",
                        ""});
            table2.AddRow(new string[] {
                        "C",
                        "1",
                        "6",
                        "5"});
            table2.AddRow(new string[] {
                        "D",
                        "0.75",
                        "",
                        ""});
#line 21
 testRunner.Given("I have products:", ((string)(null)), table2, "Given ");
#line 27
 testRunner.When(string.Format("I try to scan products {0}", scannedProducts), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 28
 testRunner.Then(string.Format("I will get exception that product {0} doesn\'t exist", notFound), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion