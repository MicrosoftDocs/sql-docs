---
title: "Programming AMO OLAP Advanced Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "programming [AMO]"
  - "Analysis Management Objects, OLAP"
  - "OLAP [AMO]"
  - "AMO, OLAP"
ms.assetid: b75f35a7-32df-4f22-983d-324aa98e15a9
caps.latest.revision: 23
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Programming AMO OLAP Advanced Objects
  This topic explains the Analysis Management Objects (AMO) programming details of OLAP advanced objects. This topic contains the following sections:  
  
-   [Action Objects](#Action)  
  
-   [Kpi Objects](#KPI)  
  
-   [Perspective Objects](#Persp)  
  
-   [ProactiveCaching Objects](#PC)  
  
-   [Translation Objects](#Transl)  
  
##  <a name="Action"></a> Action Objects  
 Action classes are used to create an active response when browsing certain areas of the cube. Action objects can be defined by using AMO, but are used from the client application that browses the data. Actions can be of different types and they have to be created according to their type. Actions can be:  
  
-   Drillthrough actions, which return the set of rows that represents the underlying data of the selected cells of the cube where the action occurs.  
  
-   Reporting actions, which return a report from [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] that is associated with the selected section of the cube where the action occurs.  
  
-   Standard actions, which return the action element (URL, HTML, DataSet, RowSet, and other elements) that is associated with the selected section of the cube where the action occurs.  
  
 Creating an action object requires the following steps:  
  
1.  Create the derived action object and populate basic attributes.  
  
     The following are the basic attributes: type of action, target type or section of the cube, target or specific area of the cube where the action is available, caption and where the caption is an MDX expression.  
  
2.  Populate the specific attributes of the action type.  
  
     Attributes are different for the three types of actions, see the code sample that follows for parameters.  
  
3.  Add the action to the cubes collection and update the cube. The action is not an updatable object.  
  
 Testing the action requires a different program application. You can test your action in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)]. First, you must install [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] samples, see [Processing a multidimensional model &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md).  
  
 The following sample code replicates three different actions from the Adventure Works Analysis Services Project sample. You can differentiate the actions because the ones that you introduce by using the following sample, start with "My".  
  
```  
static public void CreateActions(Cube cube)  
{  
    #region Adding a drillthrough action  
    // Verify That action exists and drop it  
    if (cube.Actions.ContainsName("My Reseller Details"))  
        cube.Actions.Remove("My Drillthrough Action",true);  
  
    //Create a Drillthrough action  
    DrillThroughAction dtaction = new DrillThroughAction("My Reseller Details", "My Drillthrough Action");  
  
    //Define the Action  
    dtaction.Type = ActionType.DrillThrough;  
    dtaction.TargetType = ActionTargetType.Cells;  
    dtaction.Target = "MeasureGroupMeasures(\"Reseller Sales\")";  
    dtaction.Caption = "My Drillthrough...";  
    dtaction.CaptionIsMdx = false;  
  
    #region create drillthrough action specifics  
    //Adding Drillthrough columns  
    //Adding Measure columns to the drillthrough  
    MeasureGroup mg = cube.MeasureGroups.FindByName("Reseller Sales");  
    MeasureBinding mb1 = new MeasureBinding();  
    mb1.MeasureID = mg.Measures.FindByName( "Reseller Sales Amount").ID;  
    dtaction.Columns.Add(mb1);  
  
    MeasureBinding mb2 = new MeasureBinding();  
    mb2.MeasureID = mg.Measures.FindByName("Reseller Order Quantity").ID;  
    dtaction.Columns.Add(mb2);  
  
    MeasureBinding mb3 = new MeasureBinding();  
    mb3.MeasureID = mg.Measures.FindByName("Reseller Unit Price").ID;  
    dtaction.Columns.Add(mb3);  
  
    //Adding Dimension Columns to the drillthrough  
    CubeAttributeBinding cb1 = new CubeAttributeBinding();  
    cb1.CubeID = cube.ID;  
    cb1.CubeDimensionID = cube.Dimensions.FindByName("Reseller").ID;  
    cb1.AttributeID = "Reseller Name";  
    cb1.Type = AttributeBindingType.All;  
    dtaction.Columns.Add(cb1);  
  
    CubeAttributeBinding cb2 = new CubeAttributeBinding();  
    cb2.CubeID = cube.ID;  
    cb2.CubeDimensionID = cube.Dimensions.FindByName("Product").ID;  
    cb2.AttributeID = "Product Name";  
    cb2.Type = AttributeBindingType.All;  
    dtaction.Columns.Add(cb2);  
    #endregion  
  
    //Add the defined action to the cube  
    cube.Actions.Add(dtaction);  
    #endregion  
  
    #region Adding a Standard action  
    // Verify That action exists and drop it  
    if (cube.Actions.ContainsName("My City Map"))  
        cube.Actions.Remove("My Action", true);  
  
    //Create a Drillthrough action  
    StandardAction stdaction = new StandardAction("My City Map", "My Action");  
  
    //Define the Action  
    stdaction.Type = ActionType.Url;  
    stdaction.TargetType = ActionTargetType.AttributeMembers;  
    stdaction.Target = "[Geography].[City]";  
    stdaction.Caption = "\"My View Map for \" + [Geography].[City].CurrentMember.Member_Caption + \"...\"";  
    stdaction.CaptionIsMdx = true;  
  
    #region create standard action specifics  
    stdaction.Expression = "\"http://maps.msn.com/home.aspx?plce1=\" + " +  
        "[Geography].[City].CurrentMember.Name + \",\" + " +  
        "[Geography].[State-Province].CurrentMember.Name + \",\" + " +  
        "[Geography].[Country].CurrentMember.Name + " +  
        "\"&regn1=\" + " +  
        "Case " +  
            "When [Geography].[Country].CurrentMember Is " +  
                    "[Geography].[Country].&[Australia] " +  
                "Then \"3\" " +  
            "When [Geography].[Country].CurrentMember Is " +  
                    "[Geography].[Country].&[Canada] " +  
                "Or [Geography].[Country].CurrentMember Is " +  
                    "[Geography].[Country].&[United States] " +  
                "Then \"0\" " +  
                "Else \"1\" " +  
        "End ";  
    #endregion  
  
    //Add the defined action to the cube  
    cube.Actions.Add(stdaction);  
  
    #endregion  
  
    #region Adding a Reporting action  
    // Verify That action exists and drop it  
    if (cube.Actions.ContainsName("My Sales Reason Comparisons"))  
        cube.Actions.Remove("My Report Action", true);  
  
    //Create a Report action  
    ReportAction rsaction = new ReportAction("My Sales Reason Comparisonsp", "My Report Action");  
  
    //Define the Action  
    rsaction.Type = ActionType.Report;  
    rsaction.TargetType = ActionTargetType.AttributeMembers;  
    rsaction.Target = "[Product].[Category]";  
    rsaction.Caption = "\"My Sales Reason Comparisons for \" + [Product].[Category].CurrentMember.Member_Caption + \"...\"";  
    rsaction.CaptionIsMdx = true;  
  
    #region create Report action specifics  
    rsaction.ReportServer = "MyRSSamplesServer";  
    rsaction.Path = "ReportServer?/AdventureWorks Sample Reports/Sales Reason Comparisons";  
    rsaction.ReportParameters.Add("ProductCategory", "UrlEscapeFragment( [Product].[Category].CurrentMember.UniqueName )");  
    rsaction.ReportFormatParameters.Add("rs:Command", "Render");  
    rsaction.ReportFormatParameters.Add("rs:Renderer", "HTML5");  
    #endregion  
  
    //Add the defined action to the cube  
    cube.Actions.Add(rsaction);  
  
    #endregion  
}  
```  
  
##  <a name="KPI"></a> Kpi Objects  
 A key performance indicator (KPI) is a collection of calculations that are associated with a measure group in a cube and are used to evaluate business success. <xref:Microsoft.AnalysisServices.Kpi> objects can be defined by AMO, but are used from the client application that browses the data.  
  
 Creating a <xref:Microsoft.AnalysisServices.Kpi> object requires the following steps:  
  
1.  Create the <xref:Microsoft.AnalysisServices.Kpi> object and populate the basic attributes.  
  
     The following is a list of basic attributes: Description, Display Folder, Associated Measure Group, and Value. Display Folder tells the client application where the KPI should be located for the end-user to find it. The Associated Measure Group indicates the measure group where all MDX calculations should be referred. Value shows the actual value of the performance indicator as an MDX expression.  
  
2.  Define KPI Indicators: Goal, Status, and Trend.  
  
     Indicators are MDX expressions that should evaluate between -1 to 1, but is the browsing application which defines the range of values for the indicators.  
  
3.  When you browse KPIs in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], values less than -1 are treated as -1, and values larger than 1 are treated as 1.  
  
4.  Define graphic images.  
  
     Graphic images are string values, used as reference in the client application to identify the correct set of images to display. The graphic image string also defines the behavior of the display function. Usually the range is split in an odd number of states, from bad to good, and to each state an image, from the set, is assigned.  
  
     If you use [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)] to browse your KPIs, then depending on names, the indicator range is split into either three states or five states. In addition, there are names where the range is inverted, that is -1 is 'Good' and 1 is 'Bad'. In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], three states within the range are as follows:  
  
    -   Bad = -1 to -0.5  
  
    -   OK = -0.4999 to -0.4999  
  
    -   Good = 0.50 to 1  
  
     In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], five states within the range are as follows:  
  
    -   Bad = -1 to -0.75  
  
    -   Risk = -0.7499 to -0.25  
  
    -   OK = -0.2499 to 0.2499  
  
    -   Raising = 0.25 to 0.7499  
  
    -   Good = 0.75 to 1  
  
 The following table lists the Usage, Name, and the number of states associated with the image.  
  
|Image usage|Image Name|Number of States|  
|-----------------|----------------|----------------------|  
|Status|Shapes|3|  
|Status|Traffic Light|3|  
|Status|Road Signs|3|  
|Status|Gauge|3|  
|Status|Reversed Gauge|5|  
|Status|Thermometer|3|  
|Status|Cylinder|3|  
|Status|Faces|3|  
|Status|Variance arrow|3|  
|Trend|Standard Arrow|3|  
|Trend|Status Arrow|3|  
|Trend|Reversed status arrow|5|  
|Trend|Faces|3|  
  
1.  Add the KPI to the cube collection and update the cube, because the KPI is not an updatable object.  
  
 Testing the KPI requires a different program application. You can test your KPI in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)].  
  
 The following sample code creates a KPI in the "Financial Perpective/Grow Revenue" folder for the Adventure Works cube that is included in the Adventure Works Analysis Services Project sample.  
  
```  
static public void CreateKPIs(Cube cube)  
{  
    Kpi kpi = cube.Kpis.Add("My Internet Revenue", "My Internet Revenue");  
    kpi.Description = "(My) Revenue achieved through direct sales via Interner";  
    kpi.DisplayFolder = "\\Financial Perspective\\Grow Revenue";  
    kpi.AssociatedMeasureGroupID = "Internet Sales";  
    kpi.Value = "[Measures].[Internet Sales Amount]";  
    #region Goal  
    kpi.Goal = "Case" +  
               "     When IsEmpty" +  
               "          (" +  
               "            ParallelPeriod" +  
               "            (" +  
               "              [Date].[Fiscal Time].[Fiscal Year]," +  
               "              1," +  
               "              [Date].[Fiscal Time].CurrentMember" +  
               "            )" +  
               "          )" +   
               "     Then [Measures].[Internet Sales Amount]" +  
               "     Else 1.10 *" +  
               "          (" +  
               "            [Measures].[Internet Sales Amount]," +  
               "            ParallelPeriod" +  
               "            (" +  
               "              [Date].[Fiscal Time].[Fiscal Year]," +  
               "              1," +  
               "              [Date].[Fiscal Time].CurrentMember" +  
               "            )" +  
               "          ) " +  
               " End";  
    #endregion  
    #region Status  
    kpi.Status = "Case" +  
                 "   When KpiValue( \"Internet Revenue\" ) / KpiGoal ( \"Internet Revenue\" ) >= .95 " +  
                 "   Then 1 " +  
                 "   When KpiValue( \"Internet Revenue\" ) / KpiGoal ( \"Internet Revenue\" ) <  .95 " +  
                 "        And  " +  
                 "        KpiValue( \"Internet Revenue\" ) / KpiGoal ( \"Internet Revenue\" ) >= .85 " +  
                 "   Then 0 " +  
                 "   Else -1 " +  
                 "End";  
    #endregion  
    #region Trend  
    kpi.Trend = "Case " +  
                "    When VBA!Abs " +  
                "         ( " +  
                "           KpiValue( \"Internet Revenue\" ) -  " +  
                "           ( " +  
                "             KpiValue ( \"Internet Revenue\" ), " +  
                "             ParallelPeriod " +  
                "             (  " +  
                "               [Date].[Fiscal Time].[Fiscal Year], " +  
                "               1, " +  
                "               [Date].[Fiscal Time].CurrentMember " +  
                "             ) " +  
                "           ) / " +  
                "           ( " +  
                "             KpiValue ( \"Internet Revenue\" ), " +  
                "             ParallelPeriod " +  
                "             (  " +  
                "               [Date].[Fiscal Time].[Fiscal Year], " +  
                "               1, " +  
                "               [Date].[Fiscal Time].CurrentMember " +  
                "             ) " +  
                "           )   " +  
                "         ) <=.02  " +  
                "    Then 0 " +  
                "    When KpiValue( \"Internet Revenue\" ) -  " +  
                "         ( " +  
                "           KpiValue ( \"Internet Revenue\" ), " +  
                "           ParallelPeriod " +  
                "           (  " +  
                "             [Date].[Fiscal Time].[Fiscal Year], " +  
                "             1, " +  
                "             [Date].[Fiscal Time].CurrentMember " +  
                "           ) " +  
                "         ) / " +  
                "         ( " +  
                "           KpiValue ( \"Internet Revenue\" ), " +  
                "           ParallelPeriod " +  
                "           (  " +  
                "             [Date].[Fiscal Time].[Fiscal Year], " +  
                "             1, " +  
                "             [Date].[Fiscal Time].CurrentMember " +  
                "           ) " +  
                "         )  >.02 " +  
                "    Then 1 " +  
                "    Else -1 " +  
                "End";  
    #endregion  
    kpi.TrendGraphic = "Standard Arrow";  
    kpi.StatusGraphic = "Cylinder";  
}.  
```  
  
##  <a name="Persp"></a> Perspective Objects  
 <xref:Microsoft.AnalysisServices.Perspective> objects can be defined by AMO, but are used from the client application that browses the data.  
  
 Creating a <xref:Microsoft.AnalysisServices.Perspective> object requires the following steps:  
  
1.  Create the <xref:Microsoft.AnalysisServices.Perspective> object and populate the basic attributes.  
  
     The following is a list of basic attributes: Name, Default Measure, Description, and annotations.  
  
2.  Add all objects from the parent cube that should be seen by end user.  
  
     Add cube dimensions (attributes and hierarchies), measure groups (measure and measure group), actions, KPIs, and calculations.  
  
3.  Add the perspective to the cube collection and update the cube, because perspective is not an updatable object.  
  
 Testing the perspective requires a different program application. You can test your perspective in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)].  
  
 The following code sample creates a perspective named "Direct Sales" for the supplied cube.  
  
```  
static public void CreatePerspectives(Cube cube)  
{  
    Perspective perspective = cube.Perspectives.Add("Direct Sales", "Direct Sales");  
    CubeDimension dim1 = cube.Dimensions.GetByName("Date");  
    PerspectiveDimension pdim1 = perspective.Dimensions.Add(dim1.DimensionID);  
    pdim1.Attributes.Add("Date");  
    pdim1.Attributes.Add("Calendar Year");  
    pdim1.Attributes.Add("Fiscal Year");  
    pdim1.Attributes.Add("Calendar Quarter");  
    pdim1.Attributes.Add("Fiscal Quarter");  
    pdim1.Attributes.Add("Calendar Month Number");  
    pdim1.Attributes.Add("Fiscal Month Number");  
    pdim1.Hierarchies.Add("Calendar Time");  
    pdim1.Hierarchies.Add("Fiscal Time");  
  
    CubeDimension dim2 = cube.Dimensions.GetByName("Product");  
    PerspectiveDimension pdim2 = perspective.Dimensions.Add(dim2.DimensionID);  
    pdim2.Attributes.Add("Product Name");  
    pdim2.Attributes.Add("Product Line");  
    pdim2.Attributes.Add("Model Name");  
    pdim2.Attributes.Add("List Price");  
    pdim2.Attributes.Add("Size");  
    pdim2.Attributes.Add("Weight");  
    pdim2.Hierarchies.Add("Product Model Categories");  
    pdim2.Hierarchies.Add("Product Categories");  
  
    PerspectiveMeasureGroup pmg = perspective.MeasureGroups.Add("Internet Sales");  
    pmg.Measures.Add("Internet Sales Amount");  
    pmg.Measures.Add("Internet Order Quantity");  
    pmg.Measures.Add("Internet Unit Price");  
  
    pmg = perspective.MeasureGroups.Add("Reseller Sales");  
    pmg.Measures.Add("Reseler Sales Amount");  
    pmg.Measures.Add("Reseller Order Quantity");  
    pmg.Measures.Add("Reseller Unit Price");  
  
    PerspectiveAction pact = perspective.Actions.Add("Drillthrough Action");  
  
    PerspectiveKpi pkpi = perspective.Kpis.Add("Internet Revenue");  
    Cube.Update();  
}  
```  
  
##  <a name="PC"></a> ProactiveCaching Objects  
 <xref:Microsoft.AnalysisServices.ProactiveCaching> objects can be defined by AMO.  
  
 Creating a <xref:Microsoft.AnalysisServices.ProactiveCaching> object requires the following steps:  
  
1.  Create the <xref:Microsoft.AnalysisServices.ProactiveCaching> object.  
  
     There are no basic attributes to define.  
  
2.  Add cache specifications.  
  
|Specification|Description|  
|-------------------|-----------------|  
|AggregationStorage|The type of storage for aggregations.<br /><br /> Applies to partition only. On dimension it must be **Regular.**|  
|SilenceInterval|Minimum amount of time the cache exists before the MOLAP imaging starts.|  
|Latency|The amount of time between the earliest notification and the moment when the MOLAP images are destroyed.|  
|SilenceOverrideInterval|The time after an initial notification after which the MOLAP imaging kicks in unconditionally.|  
|ForceRebuildInterval|The time (starting after a fresh MOLAP image is dropped) after which MOLAP imaging starts unconditionally (no notifications).|  
|OnlineMode|When the MOLAP image is available.<br /><br /> Can be either **Immediate** or **OnCacheComplete**.|  
  
1.  Add the <xref:Microsoft.AnalysisServices.ProactiveCaching> object to the parent collection. You will need to update the parent, because <xref:Microsoft.AnalysisServices.ProactiveCaching> is not an updatable object.  
  
 The following code sample creates a <xref:Microsoft.AnalysisServices.ProactiveCaching> object in all partitions from the Internet Sales measure group in the Adventure Works cube in a specified database.  
  
```  
static public void SetProactiveCachingSettings(Database db)  
{  
    ProactiveCaching pc;  
    if (db.Cubes.ContainsName("Adventure Works") && db.Cubes.FindByName("Adventure Works").MeasureGroups.ContainsName("Internet Sales"))  
    {  
        ProactiveCachingTablesBinding pctb;  
        TableNotification tn;  
  
        MeasureGroup mg = db.Cubes.FindByName("Adventure Works").MeasureGroups.FindByName("Internet Sales");  
        foreach(Partition part in mg.Partitions)  
        {  
            pc = new ProactiveCaching();  
            pc.AggregationStorage = ProactiveCachingAggregationStorage.MolapOnly;  
            pc.SilenceInterval = TimeSpan.FromSeconds(10);  
            pc.Latency = TimeSpan.FromSeconds(-1);  
            pc.SilenceOverrideInterval = TimeSpan.FromMinutes(10);  
            pc.ForceRebuildInterval = TimeSpan.FromSeconds(-1);  
            pc.Enabled = true;  
            pc.OnlineMode = ProactiveCachingOnlineMode.OnCacheComplete;  
            pctb = new ProactiveCachingTablesBinding();  
            pctb.NotificationTechnique = NotificationTechnique.Server;  
            tn = new TableNotification("[FactInternetSales]", "dbo");  
            pctb.TableNotifications.Add( tn);  
            pc.Source = pctb;  
  
            part.ProactiveCaching = pc;  
            part.Update();  
        }  
    }  
}  
```  
  
##  <a name="Transl"></a> Translation Objects  
 Translation objects can be defined by AMO, but are used from the client application that browses the data. Translation objects are simple objects to code. Translations for object captions are provided by pairs of Locale Identifier and Translated Caption. For any caption, multiple translations can be enabled. Translations can be provided for most [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] objects, such as dimensions, attributes, hierarchies, cubes, measure groups, measures, and others.  
  
 The following code sample provides a Spanish translation for the name of the attribute Product Name.  
  
```  
static public void CreateTranslations(Database db)  
{  
    //Spanish Tranlations for Product Category in Product Dimension  
    Dimension dim = db.Dimensions["Product"];  
    DimensionAttribute atr = dim.Attributes["Product Name"];  
    Translation tran = atr.Translations.Add(3082);  
    tran.Caption = "Nombre Producto";  
  
    dim.Update(UpdateOptions.ExpandFull);  
  
}  
```  
  
## See Also  
 <xref:Microsoft.AnalysisServices>   
 [Introducing AMO Classes](../../../analysis-services/multidimensional-models/analysis-management-objects/amo-classes-introduction.md)   
 [AMO OLAP Classes](../../../analysis-services/multidimensional-models/analysis-management-objects/amo-olap-classes.md)   
 [Logical Architecture &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/understanding-microsoft-olap-logical-architecture.md)   
 [Database Objects &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/database-objects-analysis-services-multidimensional-data.md)   
 [Processing a multidimensional model &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md)   
 [Install Sample Data and Projects for the Analysis Services Multidimensional Modeling Tutorial](../../../analysis-services/install-sample-data-and-projects.md)  
  
  