---
title: "Defining a Data Source View (Analysis Services) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Defining a Data Source View (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A data source view contains the logical model of the schema used by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] multidimensional database objects-namely cubes, dimensions, and mining structures. A data source view is the metadata definition, stored in an XML format, of these schema elements used by the Unified Dimensional Model (UDM) and by the mining structures. A data source view:  
  
-   Contains the metadata that represents selected objects from one or more underlying data sources, or the metadata that will be used to generate an underlying relational data store if you are following the top-down approach to schema generation.  
  
-   Can be built over one or more data sources, letting you define multidimensional and data mining objects that integrate data from multiple sources.  
  
-   Can contain relationships, primary keys, object names, calculated columns, and queries that are not present in an underlying data source and which exist separate from the underlying data sources.  
  
-   Is not visible to or available to be queried by client applications.  
  
 A DSV is a required component of a multidimensional model. Most Analysis Services developers create a DSV during the early phases of model design, generating at least one DSV based on an external relational database that provides underlying data. However, you can also create the DSV at a later phase, generating the schema and underlying database structures after the dimensions and cubes are created. This second approach is sometimes called top-down design and is frequently used for prototyping and analysis modeling. When you use this approach, you use the Schema Generation Wizard to create the underlying data source view and data source objects based on the OLAP objects defined in an Analysis Services project or database. Regardless of how and when you create a DSV, every model must have one before you can process it.  
  
 This topic includes the following sections:  
  
 [Data Source View Composition](#bkmk_dsvdef)  
  
 [Create a DSV Using the Data Source View Wizard](#bkmk_startWiz)  
  
 [Specify Name Matching Criteria for Relationships](#bkmk_NameMatch)  
  
 [Add a Secondary Data Source](#bkmk_secondaryDS)  
  
##  <a name="bkmk_dsvdef"></a> Data Source View Composition  
 A data source view contains the following items:  
  
-   A name and a description.  
  
-   A definition of any subset of the schema retrieved from one or more data sources, up to and including the whole schema, including the following:  
  
    -   Table names.  
  
    -   Column names.  
  
    -   Data types.  
  
    -   Nullability.  
  
    -   Column lengths.  
  
    -   Primary keys.  
  
    -   Primary key - foreign key relationships.  
  
-   Annotations to the schema from the underlying data sources, including the following:  
  
    -   Friendly names for tables, views, and columns.  
  
    -   Named queries that return columns from one or more data sources (that show as tables in the schema).  
  
    -   Named calculations that return columns from a data source (that show as columns in tables or views).  
  
    -   Logical primary keys (needed if a primary key is not defining in the underlying table or is not included in the view or named query).  
  
    -   Logical primary key - foreign key relationships between tables, views, and named queries.  
  
##  <a name="bkmk_startWiz"></a> Create a DSV Using the Data Source View Wizard  
 To create a DSV, run the Data Source View Wizard from Solution Explorer in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)].  
  
> [!NOTE]  
>  Alternatively, you can construct dimensions and cubes first, and then generate a DSV for the model using the Schema Generation wizard. For more information, see [Schema Generation Wizard &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/schema-generation-wizard-analysis-services.md).  
  
1.  In Solution Explorer, right-click the Data Source Views folder and then click **New Data Source View**.  
  
2.  Specify a new or existing data source object that provides connection information to an external relational database (you can only select one data source in the wizard).  
  
3.  On the same page, click **Advanced** to choose specific schemas, apply a filter, or exclude table relationship information.  
  
     **Choose Schemas**  
  
     For very large data sources containing multiple schemas, you can select which schemas to use in a comma delimited list, with no spaces.  
  
     **Retrieve Relationships**  
  
     You can purposely omit table relationship information by clearing the **Retrieve relationships** checkbox in the Advanced Data Source View Options dialog box, allowing you to manually create relationships between tables in the Data Source View Designer.  
  
4.  **Filter Available Objects**  
  
     If the Available objects list contains a very large number of objects, you can reduce the list by applying a simple filter that species a string as selection criteria. For example, if you type **dbo** and click the **Filter** button, then only those items starting with "dbo" show up in the **Available objects** list. The filter can be a partial string (for example, "sal" returns sales and salary) but it cannot include multiple strings or operators.  
  
5.  For relational data sources that do not have table relationships defined, a **Name Matching** page appears so that you can select the appropriate name matching method. For more information, see the [Specify Name Matching Criteria for Relationships](#bkmk_NameMatch) section in this topic.  
  
##  <a name="bkmk_secondaryDS"></a> Add a Secondary Data Source  
 When defining a data source view that contains tables, views, or columns from multiple data sources, the first data source from which you add objects to the data source view is designated as the primary data source (you cannot change the primary data source after it is defined). After defining a data source view based on objects from a single data source, you can then add objects from other data sources.  
  
 If an OLAP processing or a data mining query requires data from multiple data sources in a single query, the primary data source must support remote queries using **OpenRowset**. Typically, this will be a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source. For example, if you design an OLAP dimension that contains attributes that are bound to columns from multiple data sources, then [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will construct an **OpenRowset** query to populate this dimension during processing. However, if an OLAP object can be populated or a data mining query resolved from a single data source, then an **OpenRowset** query will not be constructed. In certain situations, you may be able to define attribute relationships between attributes to eliminate the need for an **OpenRowset** query. For more information about attribute relationships, see [Attribute Relationships](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/attribute-relationships.md), [Adding or Removing Tables or Views in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/adding-or-removing-tables-or-views-in-a-data-source-view-analysis-services.md) and [Define Attribute Relationships](../../analysis-services/multidimensional-models/attribute-relationships-define.md).  
  
 To add tables and columns from a second data source, you double-click the DSV in Solution Explorer to open it in Data Source View Designer, and then use Add/Remove Tables dialog box to include objects from other data sources that are defined in your project. For more information, see [Adding or Removing Tables or Views in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/adding-or-removing-tables-or-views-in-a-data-source-view-analysis-services.md).  
  
##  <a name="bkmk_NameMatch"></a> Specify Name Matching Criteria for Relationships  
 When you create a DSV, relationships are created between tables based on foreign key constraints in the data source. These relationships are required for the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] engine to construct the appropriate OLAP processing and data mining queries. Sometimes, however, a data source with multiple tables has no foreign key constraints. If a data source has no foreign key constraints, the Data Source View Wizard prompts you to define how you want the wizard to attempt to match column names from different tables.  
  
> [!NOTE]  
>  You are prompted to provide name matching criteria only if no foreign key relationships are detected in the underlying data source. If foreign key relationships are detected, then the detected relationships are used and you must manually define any additional relationships you want to include in the DSV, including logical primary keys. For more information, see [Define Logical Relationships in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/define-logical-relationships-in-a-data-source-view-analysis-services.md) and [Define Logical Primary Keys in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/define-logical-primary-keys-in-a-data-source-view-analysis-services.md).  
  
 The Data Source View Wizard uses your response to match column names and create relationships between different tables in the DSV. You can specify any one of the criteria listed in the following table.  
  
|Name matching criteria|Description|  
|----------------------------|-----------------|  
|**Same name as primary key**|The foreign key column name in the source table is the same as the primary key column name in the destination table. For example, the foreign key column `Order.CustomerID` is the same as the primary key column `Customer.CustomerID`.|  
|**Same name as destination table name**|The foreign key column name in the source table is the same as the name of the destination table. For example, the foreign key column `Order.Customer` is the same as the primary key column `Customer.CustomerID`.|  
|**Destination table name + primary key name**|The foreign key column name in the source table is the same as the destination table name concatenated with the primary key column name. A space or underscore separator is permissible. For example, the following foreign-primary key pairs all match:<br /><br /> `Order.CustomerID` and `Customer.ID`<br /><br /> `Order.Customer ID` and `Customer.ID`<br /><br /> `Order.Customer_ID` and `Customer.ID`|  
  
 The criteria you select changes the **NameMatchingCriteria** property setting of the DSV. This setting determines how the wizard adds related tables. When you change the data source view with Data Source View Designer, this specification determines how the designer matches columns to create relationships between tables in the DSV. You can change the **NameMatchingCriteria** property setting in Data Source View Designer. For more information, see [Change Properties in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/change-properties-in-a-data-source-view-analysis-services.md).  
  
> [!NOTE]  
>  After you complete the Data Source View Wizard, you can add or remove relationships in the schema pane of Data Source View Designer. For more information, see [Define Logical Relationships in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/define-logical-relationships-in-a-data-source-view-analysis-services.md).  
  
## See Also  
 [Adding or Removing Tables or Views in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/adding-or-removing-tables-or-views-in-a-data-source-view-analysis-services.md)   
 [Define Logical Primary Keys in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/define-logical-primary-keys-in-a-data-source-view-analysis-services.md)   
 [Define Named Calculations in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/define-named-calculations-in-a-data-source-view-analysis-services.md)   
 [Define Named Queries in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/define-named-queries-in-a-data-source-view-analysis-services.md)   
 [Replace a Table or a Named Query in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/replace-a-table-or-a-named-query-in-a-data-source-view-analysis-services.md)   
 [Work with Diagrams in Data Source View Designer &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/work-with-diagrams-in-data-source-view-designer-analysis-services.md)   
 [Explore Data in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/explore-data-in-a-data-source-view-analysis-services.md)   
 [Delete a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/delete-a-data-source-view-analysis-services.md)   
 [Refresh the Schema in a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/refresh-the-schema-in-a-data-source-view-analysis-services.md)  
  
  
