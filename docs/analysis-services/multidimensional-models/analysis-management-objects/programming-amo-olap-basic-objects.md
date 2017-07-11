---
title: "Programming AMO OLAP Basic Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
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
ms.assetid: ad1c970e-c0cb-4687-9563-56ab62c2db5f
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Programming AMO OLAP Basic Objects
  Creating complex [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] objects is simple and straightforward but requires attention to detail. This topic explains the programming details of OLAP basic objects. This topic contains the following sections:  
  
-   [Dimension Objects](#Dim)  
  
-   [Cube Objects](#Cub)  
  
-   [MeasureGroup Objects](#MG)  
  
-   [Partition Objects](#Part)  
  
-   [Aggregation Objects](#AD)  
  
##  <a name="Dim"></a> Dimension Objects  
 To administer or process a dimension, you program the <xref:Microsoft.AnalysisServices.Dimension> object.  
  
### Creating, Dropping, and Finding a Dimension  
 Creating a <xref:Microsoft.AnalysisServices.Dimension> object is accomplished in four steps:  
  
1.  Create the dimension object and populate basic attributes.  
  
     Basic attributes are Name, Dimension Type, Storage Mode, Data Source Binding, Attribute All Member Name, and other dimension attributes.  
  
     Before creating a dimension, you should verify that the dimension does not already exist. If the dimension exists, then the dimension is dropped and re-created.  
  
2.  Create the attributes that define the dimension.  
  
     Each attribute has to be added individually to the schema before using it (find CreateDataItem method at the end of the sample code), and then can be added to the attributes collection of the dimension.  
  
     Key and Name column must be defined in all attributes.  
  
     The primary key attribute of the dimension should be defined as AttributeUsage.Key to make clear that this attribute is the key access to the dimension.  
  
3.  Create the hierarchies that the user will access to navigate the dimension.  
  
     When you are creating hierarchies, the level order is defined by the order in which levels are created from top to bottom. The highest level is the first added to the levels collection of the hierarchy.  
  
4.  Update the server by using the Update method of the current dimension.  
  
 The following sample code creates the Product dimension from the Adventure Works products table in the sample database.  
  
```  
static void CreateProductDimension(Database db, string datasourceName)  
{  
    // Create the Product dimension  
    Dimension dim = db.Dimensions.FindByName("Product");  
    if ( dim != null)  
       dim.Drop();  
    dim = db.Dimensions.Add("Product");  
    dim.Type = DimensionType.Products;  
    dim.UnknownMember = UnknownMemberBehavior.Hidden;  
    dim.AttributeAllMemberName = "All Products";  
    dim.Source = new DataSourceViewBinding(datasourceName);  
    dim.StorageMode = DimensionStorageMode.Molap;  
  
    #region Create attributes  
  
    DimensionAttribute attr;  
  
    attr = dim.Attributes.Add("Product Name");  
    attr.Usage = AttributeUsage.Key;  
    attr.Type = AttributeType.Product;  
    attr.OrderBy = OrderBy.Name;  
    attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimProduct", "ProductKey"));  
    attr.NameColumn = CreateDataItem(db.DataSourceViews[0], "DimProduct", "EnglishProductName");  
  
    attr = dim.Attributes.Add("Product Line");  
    attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimProduct", "ProductLine"));  
    attr.NameColumn = CreateDataItem(db.DataSourceViews[0], "DimProduct", "ProductLineName");  
  
    attr = dim.Attributes.Add("Model Name");  
    attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimProduct", "ModelName"));  
    attr.AttributeRelationships.Add(new AttributeRelationship("Product Line"));  
    attr.AttributeRelationships.Add(new AttributeRelationship("Subcategory"));  
  
    attr = dim.Attributes.Add("Subcategory");  
    attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimProductSubcategory", "ProductSubcategoryKey"));  
    attr.KeyColumns[0].NullProcessing = NullProcessing.UnknownMember;  
    attr.NameColumn = CreateDataItem(db.DataSourceViews[0], "DimProductSubcategory", "EnglishProductSubcategoryName");  
    attr.AttributeRelationships.Add(new AttributeRelationship("Category"));  
  
    attr = dim.Attributes.Add("Category");  
    attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimProductCategory", "ProductCategoryKey"));  
    attr.NameColumn = CreateDataItem(db.DataSourceViews[0], "DimProductCategory", "EnglishProductCategoryName");  
  
    attr = dim.Attributes.Add("List Price");  
    attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimProduct", "ListPrice"));  
    attr.AttributeHierarchyEnabled = false;  
  
    attr = dim.Attributes.Add("Size");  
    attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimProduct", "Size"));  
    attr.AttributeHierarchyEnabled = false;  
  
    attr = dim.Attributes.Add("Weight");  
    attr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "DimProduct", "Weight"));  
    attr.AttributeHierarchyEnabled = false;  
  
    #endregion  
  
    #region Create hierarchies  
  
    Hierarchy hier;  
  
    hier = dim.Hierarchies.Add("Product Model Categories");  
    hier.AllMemberName = "All Products";  
    hier.Levels.Add("Category").SourceAttributeID = "Category";  
    hier.Levels.Add("Subcategory").SourceAttributeID = "Subcategory";  
    hier.Levels.Add("Model Name").SourceAttributeID = "Model Name";  
  
    hier = dim.Hierarchies.Add("Product Categories");  
    hier.AllMemberName = "All Products";  
    hier.Levels.Add("Category").SourceAttributeID = "Category";  
    hier.Levels.Add("Subcategory").SourceAttributeID = "Subcategory";  
    hier.Levels.Add("Model Name").SourceAttributeID = "Product Name";  
  
    hier = dim.Hierarchies.Add("Product Model Lines");  
    hier.AllMemberName = "All Products";  
    hier.Levels.Add("Subcategory").SourceAttributeID = "Product Line";  
    hier.Levels.Add("Model Name").SourceAttributeID = "Model Name";  
  
    #endregion  
  
    dim.Update();  
}  
  
static DataItem CreateDataItem(DataSourceView dsv, string tableName, string columnName)  
{  
    DataTable dataTable = ((DataSourceView)dsv).Schema.Tables[tableName];  
    DataColumn dataColumn = dataTable.Columns[columnName];  
    return new DataItem(tableName, columnName,  
        OleDbTypeConverter.GetRestrictedOleDbType(dataColumn.DataType));  
}  
  
```  
  
### Processing a Dimension  
 Processing a dimension is as simple as using the Process method of the <xref:Microsoft.AnalysisServices.Dimension> object.  
  
 Processing a dimension can affect all cubes that use the dimension. For more information about processing options, see [Processing a multidimensional model &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md).  
  
 The following code does an incremental update in all dimensions of a supplied database:  
  
```  
static void UpdateAllDimensions(Database db)  
{  
    foreach (Dimension dim in db.Dimensions)  
        dim.Process(ProcessType.ProcessUpdate);  
}  
```  
  
##  <a name="Cub"></a> Cube Objects  
 To administer or process a cube, you program the <xref:Microsoft.AnalysisServices.Cube> object.  
  
### Creating, Dropping, and Finding a Cube  
 Managing cubes is similar to managing dimensions. Creating a <xref:Microsoft.AnalysisServices.Cube> object is accomplished in four steps:  
  
1.  Create the cube object and populate basic attributes.  
  
     Basic attributes are Name, Storage Mode, Data Source Binding, Default Measure, and other cube attributes.  
  
     Before creating a cube you should verify that the cube does not exist. In the sample if the cube exists the cube is dropped and then re-created.  
  
2.  Add the dimensions of the cube.  
  
     Dimensions are added to the current cube dimensions collection from the database; dimensions in the cube are references to the database dimensions collection. Each dimension has to be mapped to the cube individually. In the sample dimensions are mapped providing: the database dimension Internal Identifier, a Name for the dimension in the cube and an Id for the named dimension in the cube.  
  
     In the sample code notice that "Date" dimension is added three times, every time is added by using a different cube dimension name: Date, Ship Date, Delivery Date. These dimensions are called “role playing” dimensions. The base dimension is the same (Date), but in the fact table the dimension is used in different “roles” (Order Date, Ship Date, Delivery Date) -see "Creating, dropping and finding a MeasureGroup" later in this document to understand how "role playing" dimensions are defined.  
  
3.  Create the Measure Groups that the user will access to browse the data of the cube.  
  
     Measure group creation will be explained in "Creating, dropping and finding a MeasureGroup" later in this document. The sample wraps measure group creation in different methods, one for each measure group.  
  
4.  Update the server by using the Update method of current cube.  
  
     The update method is used with the Update option ExpandFull to make sure that all objects are fully updated in the server.  
  
 The following code sample creates the parts of the Adventure Works cube. The code sample does not create all dimensions or measure groups that are included in the Adventure Works Analysis Services Project sample.  
  
```  
static void CreateAdventureWorksCube(Database db, string datasourceName)  
{  
    // Create the Adventure Works cube  
    Cube cube = db.Cubes.FindByName("Adventure Works");  
    if ( cube != null)  
       cube.Drop();  
    db.Cubes.Add("Adventure Works");  
    cube.DefaultMeasure = "[Reseller Sales Amount]";  
    cube.Source = new DataSourceViewBinding(datasourceName);  
    cube.StorageMode = StorageMode.Molap;  
  
    #region Create cube dimensions  
  
    Dimension dim;  
  
    dim = db.Dimensions.GetByName("Date");  
    cube.Dimensions.Add(dim.ID, "Date", "Order Date Key - Dim Time");  
    cube.Dimensions.Add(dim.ID, "Ship Date",  
        "Ship Date Key - Dim Time");  
    cube.Dimensions.Add(dim.ID, "Delivery Date",  
        "Delivery Date Key - Dim Time");  
  
    dim = db.Dimensions.GetByName("Customer");  
    cube.Dimensions.Add(dim.ID);  
  
    dim = db.Dimensions.GetByName("Reseller");  
    cube.Dimensions.Add(dim.ID);  
    #endregion  
  
    #region Create measure groups  
  
    CreateSalesReasonsMeasureGroup(cube);  
    CreateInternetSalesMeasureGroup(cube);  
    CreateResellerSalesMeasureGroup(cube);  
    CreateCustomersMeasureGroup(cube);  
    CreateCurrencyRatesMeasureGroup(cube);  
  
    #endregion  
  
    cube.Update(UpdateOptions.ExpandFull);  
}  
```  
  
### Processing a Cube  
 Processing a cube is as simple as using the Process method of the <xref:Microsoft.AnalysisServices.Cube> object. Processing a cube also processes all measure groups in the cube, and all partitions in the measure group. In a cube, partitions are the only objects that can be processed; for the purposes of processing, measure groups are only containers of partitions. The specified type of processing for the cube propagates to the partitions. Processing of cube and measure group internally is resolved to processing of dimensions and partitions.  
  
 For more information about processing options, see [Processing Objects &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/processing-objects-xmla.md), and [Processing a multidimensional model &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md).  
  
 The following code will do a full process on all cubes in a specified database:  
  
```  
foreach (Cube cube in db.Cubes)  
             cube.Process(ProcessType.ProcessFull);  
     }  
```  
  
##  <a name="MG"></a> MeasureGroup Objects  
 To administer or process a measure group, you program the <xref:Microsoft.AnalysisServices.MeasureGroup> object.  
  
### Creating, Dropping, and Finding a MeasureGroup  
 Managing measure groups is similar to managing dimensions and cubes. Creating a <xref:Microsoft.AnalysisServices.MeasureGroup> object is accomplished in the following steps:  
  
1.  Create the measure group object and populate the basic attributes.  
  
     Basic attributes include Name, Storage Mode, Processing Mode, Default Measure, and other measure group attributes.  
  
     Before creating a measure group, verify that the measure group does not exist. In the sample code that follows, if the measure group exists, then the measure group is dropped and re-created.  
  
2.  Create the measures of the measure group. For each measure created, the following attributes are assigned: name, aggregation function, source column, format string. Other attributes can also be assigned. Note that in the sample code that follows, the CreateDataItem method adds the column to the schema.  
  
3.  Add the dimensions of the measure group.  
  
4.  Dimensions are added to the current measure group dimensions collection from the parent cube dimensions collection. As soon as the dimension is included in the measure group dimensions collection, a key column from the fact table can be mapped to the dimension so that the measure group can be browsed through the dimension.  
  
     In the sample code that follows, see the lines under "Mapping dimension and key column from fact table". The role playing dimensions are implemented by linking different surrogate keys to the same dimension under different names. For each one of the role playing dimensions (Date, Ship Date, Delivery Date), a different surrogate key is linked to it (OrderDateKey, ShipDateKey, DueDateKey). All keys are from the fact table FactInternetSales.  
  
5.  Add the designed partitions of the measure group.  
  
     The in the sample code that follows, partition creation is wrapped in one method.  
  
6.  Update the server by using the Update method of current measure group.  
  
     In the sample code that follows, all measure groups are updated when the cube is updated.  
  
 The following sample code will create the InternetSales measure group of the Adventure Works Analysis Services Project sample.  
  
```  
static void CreateInternetSalesMeasureGroup(Cube cube)  
{  
    // Create the Internet Sales measure group  
    Database db = cube.Parent;  
    MeasureGroup mg = cube.MeasureGroups.FindByName("Internet Sales");  
    if ( mg != null)  
       mg.Drop();  
    mg = cube.MeasureGroups.Add("Internet Sales");  
    mg.StorageMode = StorageMode.Molap;  
    mg.ProcessingMode = ProcessingMode.LazyAggregations;  
    mg.Type = MeasureGroupType.Sales;  
  
    #region Create measures  
  
    Measure meas;  
  
    meas = mg.Measures.Add("Internet Sales Amount");  
    meas.AggregateFunction = AggregationFunction.Sum;  
    meas.FormatString = "Currency";  
    meas.Source = CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "SalesAmount");  
  
    meas = mg.Measures.Add("Internet Order Quantity");  
    meas.AggregateFunction = AggregationFunction.Sum;  
    meas.FormatString = "#,#";  
    meas.Source = CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "OrderQuantity");  
  
    meas = mg.Measures.Add("Internet Unit Price");  
    meas.AggregateFunction = AggregationFunction.Sum;  
    meas.FormatString = "Currency";  
    meas.Visible = false;  
    meas.Source = CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "UnitPrice");  
  
    meas = mg.Measures.Add("Internet Total Product Cost");  
    meas.AggregateFunction = AggregationFunction.Sum;  
    //meas.MeasureExpression = "[Internet Total Product Cost] * [Average Rate]";  
    meas.FormatString = "Currency";  
    meas.Source = CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "TotalProductCost");  
  
    meas = mg.Measures.Add("Internet Order Count");  
    meas.AggregateFunction = AggregationFunction.Count;  
    meas.FormatString = "#,#";  
    meas.Source = CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "ProductKey");  
  
    #endregion  
  
    #region Create measure group dimensions  
  
    CubeDimension cubeDim;  
    RegularMeasureGroupDimension regMgDim;  
    ManyToManyMeasureGroupDimension mmMgDim;  
    MeasureGroupAttribute mgAttr;  
  
    //   Mapping dimension and key column from fact table  
    //      > select dimension and add it to the measure group  
    cubeDim = cube.Dimensions.GetByName("Date");  
    regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);  
    mg.Dimensions.Add(regMgDim);  
  
    //      > add key column from dimension and map it with   
    //        the surrogate key in the fact table  
    mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Date").ID);   // this is dimension key column  
    mgAttr.Type = MeasureGroupAttributeType.Granularity;  
    mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "OrderDateKey"));   // this surrogate key in fact table  
  
    cubeDim = cube.Dimensions.GetByName("Ship Date");  
    regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);  
    mg.Dimensions.Add(regMgDim);  
    mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Date").ID);  
    mgAttr.Type = MeasureGroupAttributeType.Granularity;  
    mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "ShipDateKey"));  
  
    cubeDim = cube.Dimensions.GetByName("Delivery Date");  
    regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);  
    mg.Dimensions.Add(regMgDim);  
    mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Date").ID);  
    mgAttr.Type = MeasureGroupAttributeType.Granularity;  
    mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "DueDateKey"));  
  
    cubeDim = cube.Dimensions.GetByName("Customer");  
    regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);  
    mg.Dimensions.Add(regMgDim);  
    mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Full Name").ID);  
    mgAttr.Type = MeasureGroupAttributeType.Granularity;  
    mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "CustomerKey"));  
  
    cubeDim = cube.Dimensions.GetByName("Product");  
    regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);  
    mg.Dimensions.Add(regMgDim);  
    mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Product Name").ID);  
    mgAttr.Type = MeasureGroupAttributeType.Granularity;  
    mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "ProductKey"));  
  
    cubeDim = cube.Dimensions.GetByName("Source Currency");  
    regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);  
    mg.Dimensions.Add(regMgDim);  
    mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Currency").ID);  
    mgAttr.Type = MeasureGroupAttributeType.Granularity;  
    mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "CurrencyKey"));  
  
    cubeDim = cube.Dimensions.GetByName("Sales Reason");  
    mmMgDim = new ManyToManyMeasureGroupDimension();  
    mmMgDim.CubeDimensionID = cubeDim.ID;  
    mmMgDim.MeasureGroupID = cube.MeasureGroups.GetByName("Sales Reasons").ID;  
    mg.Dimensions.Add(mmMgDim);  
  
    cubeDim = cube.Dimensions.GetByName("Internet Sales Order Details");  
    regMgDim = new RegularMeasureGroupDimension(cubeDim.ID);  
    mg.Dimensions.Add(regMgDim);  
    mgAttr = regMgDim.Attributes.Add(cubeDim.Dimension.Attributes.GetByName("Sales Order Key").ID);  
    mgAttr.Type = MeasureGroupAttributeType.Granularity;  
    mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "SalesOrderNumber"));  
    mgAttr.KeyColumns.Add(CreateDataItem(db.DataSourceViews[0], "FactInternetSales", "SalesOrderLineNumber"));  
  
    #endregion  
  
    #region Create partitions  
  
    CreateInternetSalesMeasureGroupPartitions( mg)  
  
    #endregion  
}  
```  
  
### Processing a Measure Group  
 Processing a measure group is as simple as using the Process method of the <xref:Microsoft.AnalysisServices.MeasureGroup> object. Processing a measure group will process all partitions that belong to the measure group. Processing a measure group internally is resolved to processing dimensions and partitions. See [Processing a Partition](#ProcPart) in this document.  
  
 For more information about processing options, see [Processing Objects &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/processing-objects-xmla.md), and [Processing a multidimensional model &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md).  
  
 The following code will do a full process in all measure groups of a supplied cube.  
  
```  
static void FullProcessAllMeasureGroups(Cube cube)  
{  
    foreach (MeasureGroup mg in cube.MeasureGroups)  
        mg.Process(ProcessType.ProcessFull);  
}  
```  
  
##  <a name="Part"></a> Partition Objects  
 To administer or process a partition, you program a <xref:Microsoft.AnalysisServices.Partition> object.  
  
### Creating, Dropping, and Finding a Partition  
 Partitions are simple objects that can be created in two steps.  
  
1.  Create the partition object and populate the basic attributes.  
  
     Basic attributes are Name, Storage Mode, partition source, Slice, as well as other measure group attributes. Partition source defines the SQL select statement for current partition. Slice is an MDX expression specifying a tuple or a set that delimits a part of the dimensions from the parent measure group that are contained in the current partition. For MOLAP partitions, slicing is determined automatically every time that the partition is processed.  
  
     Before creating a partition, you should verify that the partition does not exist. In the sample code that follows, if the partition exists, it is dropped and then re-created.  
  
2.  Update the server by using the Update method of the current partition.  
  
     In the sample code that follows, all partitions are updated when the cube is updated.  
  
 The following code sample creates partitions for the 'InternetSales' measure group.  
  
```  
static void CreateInternetSalesMeasureGroupPartitions(MeasureGroup mg)  
{  
    Partition part;  
    part = mg.Partitions.FindByName("Internet_Sales_184");  
    if ( part != null)  
       part.Drop();  
    part = mg.Partitions.Add("Internet_Sales_184");  
    part.StorageMode = StorageMode.Molap;  
    part.Source = new QueryBinding(db.DataSources[0].ID, "SELECT * FROM [dbo].[FactInternetSales] WHERE OrderDateKey <= '184'");  
    part.Slice = "[Date].[Calendar Year].&[2001]";  
    part.Annotations.Add("LastOrderDateKey", "184");  
  
    part = mg.Partitions.FindByName("Internet_Sales_549");  
    if ( part != null)  
       part.Drop();  
    part = mg.Partitions.Add("Internet_Sales_549");  
    part.StorageMode = StorageMode.Molap;  
    part.Source = new QueryBinding(db.DataSources[0].ID, "SELECT * FROM [dbo].[FactInternetSales] WHERE OrderDateKey > '184' AND OrderDateKey <= '549'");  
    part.Slice = "[Date].[Calendar Year].&[2002]";  
    part.Annotations.Add("LastOrderDateKey", "549");  
  
    part = mg.Partitions.FindByName("Internet_Sales_914");  
    if ( part != null)  
       part.Drop();  
    part = mg.Partitions.Add("Internet_Sales_914");  
    part.StorageMode = StorageMode.Molap;  
    part.Source = new QueryBinding(db.DataSources[0].ID, "SELECT * FROM [dbo].[FactInternetSales] WHERE OrderDateKey > '549' AND OrderDateKey <= '914'");  
    part.Slice = "[Date].[Calendar Year].&[2003]";  
    part.Annotations.Add("LastOrderDateKey", "914");  
}  
```  
  
###  <a name="ProcPart"></a> Processing a Partition  
 Processing a partition is as simple as using the Process method of the <xref:Microsoft.AnalysisServices.Partition> object.  
  
 For more information about processing options, see [Processing Objects &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/processing-objects-xmla.md) and [Processing a multidimensional model &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md).  
  
 The following code sample does a full process in all partitions of a specified measure group.  
  
```  
static void FullProcessAllPartitions(MeasureGroup mg)  
{  
    foreach (Partition part in mg.Partitions)  
        part.Process(ProcessType.ProcessFull);  
}  
```  
  
### Merging Partitions  
 Merging partitions means performing any operation that results in two or more partitions becoming one partition.  
  
 Merging partitions is a method of the <xref:Microsoft.AnalysisServices.Partition> object. This command merges the data of one or more source partitions into a target partition and deletes the source partitions.  
  
 Partitions can be merged only if they meet all the following criteria:  
  
-   Partitions are in the same measure group.  
  
-   Partitions are stored in the same mode (MOLAP, HOLAP, and ROLAP).  
  
-   Partitions reside on the same server; remote partitions can be merged if on the same server.  
  
 Unlike previous versions, in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] it is not necessary that all source partitions have identical aggregations design.  
  
 The resulting set of aggregations for the target partition is the same set of aggregations as of the state before running merge command.  
  
 The following code sample merges all partitions of a specified measure group. The partitions are merged into the first partition of the measure group.  
  
```  
static void MergeAllPartitions(MeasureGroup mg)  
{  
    if (mg.Partitions.Count > 1)  
    {  
        Partition[] partArray = new Partition[mg.Partitions.Count - 1];  
        for (int i = 1; i < mg.Partitions.Count; i++)  
            partArray[i - 1] = mg.Partitions[i];  
        mg.Partitions[0].Merge(partArray);  
        //To have last changes in the server reflected in AMO  
        mg.Refresh();  
    }  
```  
  
##  <a name="AD"></a> Aggregation Objects  
 To design an design an aggregation and apply it to one or more partitions, you program <xref:Microsoft.AnalysisServices.Aggregation> object.  
  
### Creating and Dropping Aggregations  
 Aggregations can easily be created and assigned to measure groups or to partitions by using the DesignAggregations method from the <xref:Microsoft.AnalysisServices.AggregationDesign> object. The <xref:Microsoft.AnalysisServices.AggregationDesign> object is a separate object from partition, the <xref:Microsoft.AnalysisServices.AggregationDesign> object is contained in the <xref:Microsoft.AnalysisServices.MeasureGroup> object. Aggregations can be designed up to specified level of optimization (0 to 100) or up to specified level of storage (bytes). Multiple partitions can use the same aggregation design.  
  
 The following code sample creates aggregations for all partitions of a supplied measure group. Any existing aggregations in partitions are dropped.  
  
```  
static public String DesignAggregationsOnPartitions(MeasureGroup mg, double optimizationWanted, double maxStorageBytes)  
{  
    double optimization = 0;  
    double storage = 0;  
    long aggCount = 0;  
    bool finished = false;  
    AggregationDesign ad = null;  
    String aggDesignName;  
    String AggregationsDesigned = "";  
    aggDesignName = mg.AggregationPrefix + "_" + mg.Name;  
    ad = mg.AggregationDesigns.Add();  
    ad.Name = aggDesignName;  
    ad.InitializeDesign();  
    while ((!finished) && (optimization < optimizationWanted) && (storage < maxStorageBytes))  
    {  
        ad.DesignAggregations(out optimization, out storage, out aggCount, out finished);  
    }  
    ad.FinalizeDesign();  
    foreach (Partition part in mg.Partitions)  
    {  
        part.AggregationDesignID = ad.ID;  
        AggregationsDesigned += aggDesignName + " = " + aggCount.ToString() + " aggregations designed\r\n\tOptimization: " + optimization.ToString() + "/" + optimizationWanted.ToString() + "\n\r\tStorage: " + storage.ToString() + "/" + maxStorageBytes.ToString() + " ]\n\r";  
     }  
     return AggregationsDesigned;  
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
  
  