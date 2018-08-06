---
title: "Create a Dimension by Using an Existing Table | Microsoft Docs"
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
# Create a Dimension by Using an Existing Table
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you can use the Dimension Wizard in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to create a dimension from an existing table. You do this by selecting the **Use an existing table** option on the **Select Creation Method** page of the wizard. If you select this option, the wizard bases the dimension structure on the dimension tables, their columns, and any relationships between those columns in an existing data source view. The wizard samples the data in the source table and related tables. It uses this data to define attribute columns that are based on the columns in the dimension tables, and to define hierarchies of attributes (called *user-defined* hierarchies). After you use the Dimension Wizard to create your dimension, you can use Dimension Designer to add, remove, and configure attributes and hierarchies in the dimension.  
  
 When you are using an existing table to create a dimension, the Dimension Wizard guides you through the following:  
  
-   Specifying the source information  
  
-   Selecting related tables  
  
-   Selecting dimension attributes  
  
-   Defining account intelligence  
  
> [!NOTE]  
>  For the step-by-step instructions that correspond to the information presented in this topic, see [Create a Dimension Using the Dimension Wizard](../../analysis-services/multidimensional-models/create-a-dimension-using-the-dimension-wizard.md).  
  
## Specifying the Source Information  
 You specify the source information on the **Specify Source Information** page. You begin this process by selecting the data source view that contains the table on which you want the dimension to be based. You then specify the main dimension table for the dimension that you are defining. The main dimension table is the table that is directly linked to the fact table. For example, specify a Product table as the main table for a Products dimension, or an Employee table for an Employees dimension. The wizard automatically selects a key column that is based on the primary key in the data source view. However, you can change the key column as appropriate. The key column determines the members of the dimension. For example, you would define ProductKey as the key column for a Product dimension.  
  
 Optionally, you can define a column that contains the member name. By default, the member name that will be displayed to users is the value from the key column. The values in a key column, such as ProductID or EmployeeID, are often unique, system-generated keys that are meaningless to the user. You can often provide more meaningful information to the user if you change the name that users see to a corresponding value in some other column in the dimension. For example, you can define a member name column that contains product or employee names. If you change the member name, users see a more descriptive name, but queries still use the key column values to correctly distinguish members that share the same name. If you specify a composite key for the key column, you must also specify the column that provides the member values for the key attribute. For more information about how to configure attribute properties, see [Dimension Attribute Properties Reference](../../analysis-services/multidimensional-models/dimension-attribute-properties-reference.md).  
  
## Selecting Related Tables  
  
> [!NOTE]  
>  The wizard skips this step if the main dimension table has no relationships defined in the data source view to other dimension tables.  
  
 If you are building a snowflake dimension, you specify the related tables from which additional attributes will be defined on the **Select Related Tables** page. For example, you are building a customer dimension in which you want to define a customer geography table. In this case, you might define a geography table as a related table.  
  
## Selecting Dimension Attributes  
 After selecting the dimension tables, you use the **Select Dimension Attributes** page to select the attributes that you want to include in the dimension from these tables. All of the underlying columns from all these tables are available as potential dimension attributes. The dimension key attribute must be selected and enabled for browsing.  
  
 By default, the wizard sets the type of an attribute to **Regular**. However, you might want to map specific attributes to a different attribute type that better represents the data. For example, the dbo.DimAccount table in the [!INCLUDE[ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)] DW sample database contains an AccountCodeAlternateKey column that provides the account number. Instead of setting the type to **Regular** for this attribute, you might want to map this attribute to the **Account Number** type.  
  
> [!NOTE]  
>  If the dimension type and standard attribute types are not set when you create the dimension, use the Business Intelligence Wizard to set these values after you create the dimension. For more information, see [Add Dimension Intelligence to a Dimension](../../analysis-services/multidimensional-models/bi-wizard-add-dimension-intelligence-to-a-dimension.md) or (for an Accounts type dimension) [Add Account Intelligence to a Dimension](../../analysis-services/multidimensional-models/bi-wizard-add-account-intelligence-to-a-dimension.md).  
  
 The wizard automatically sets the dimension type based on the attribute types that are specified. The attribute types specified in the wizard set the **Type** property for the attributes. The **Type** property settings for the dimension and its attributes provide information about the contents of a dimension to server and client applications. In some cases, these **Type** property settings only provide guidance for client applications and are optional. In other cases, as for Accounts, Time, or Currency dimensions, these **Type** property settings determine specific server-based behavior and might be required to implement certain cube behavior.  
  
 For more information about dimension and attribute types, see [Dimension Types](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/database-dimension-properties-types.md), [Configure Attribute Types](../../analysis-services/multidimensional-models/attribute-properties-configure-attribute-types.md).  
  
## Defining Account Intelligence  
  
> [!NOTE]  
>  The Dimension Wizard displays this step only if you defined an **Account Type** dimension attribute on the **Select Dimension Attributes** page of the wizard.  
  
 You use the **Define Account Intelligence** page to create an Account type dimension. If you are creating an Account type dimension, you have to map standard account types supported by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to members of the account type attribute in the dimension. The server uses these mappings to provide separate aggregation functions and aliases for each type of account data.  
  
 To map these account types, the wizard provides a table with the following columns:  
  
-   The **Source Table Account Types** column lists account types from the data source table.  
  
-   The **Built-In Account Types** column lists the corresponding standard account types supported by the server. If the source data uses standard names, the wizard automatically maps the source type to the server type, and populates the **Built-In Account Types** column with this information. If the server does not map the account types or you want to change the mapping, select a different type from the list in the **Built-In Account Types** column.  
  
> [!NOTE]  
>  If the account types are not mapped when the wizard creates an Accounts dimension, use the Business Intelligence Wizard to configure these mappings after you create the dimension. For more information, see [Add Account Intelligence to a Dimension](../../analysis-services/multidimensional-models/bi-wizard-add-account-intelligence-to-a-dimension.md).  
  
## Completing the Wizard  
 The wizard scans dimension tables to detect relationships. The wizard will create attribute relationships between key attributes in snowflake dimensions automatically.  
  
 The wizard also automatically detects if a parent-child relationship exists in the dimension. A parent-child relationship exists when a parent attribute references members of the key attribute of the dimension. This relationship defines hierarchical relationships and aggregation paths between leaf members of the dimension. For more information about parent-child hierarchies, see [Attributes in Parent-Child Hierarchies](../../analysis-services/multidimensional-models/parent-child-dimension-attributes.md).  
  
 On the **Completing the Wizard** page, you complete the wizard by typing a name for the new dimension and reviewing the dimension structure.  
  
## See Also  
 [Create a Dimension by Generating a Non-Time Table in the Data Source](../../analysis-services/multidimensional-models/create-a-dimension-by-generating-a-non-time-table-in-the-data-source.md)   
 [Create a Time Dimension by Generating a Time Table](../../analysis-services/multidimensional-models/create-a-time-dimension-by-generating-a-time-table.md)   
 [Dimension Attribute Properties Reference](../../analysis-services/multidimensional-models/dimension-attribute-properties-reference.md)   
 [Create a Time Dimension by Generating a Time Table](../../analysis-services/multidimensional-models/create-a-time-dimension-by-generating-a-time-table.md)   
 [Create a Dimension by Generating a Non-Time Table in the Data Source](../../analysis-services/multidimensional-models/create-a-dimension-by-generating-a-non-time-table-in-the-data-source.md)  
  
  
