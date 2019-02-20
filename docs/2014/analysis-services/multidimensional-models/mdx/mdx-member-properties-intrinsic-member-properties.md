---
title: "Intrinsic Member Properties (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "intrinsic member properties [MDX]"
ms.assetid: 84e6fe64-9b37-4e79-bedf-ae02e80bfce8
author: minewiskan
ms.author: owend
manager: craigg
---
# Intrinsic Member Properties (MDX)
  [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] exposes intrinsic properties on dimension members that you can include in a query to return additional data or metadata for use in a custom application, or to assist in model investigation or construction. If you are using the SQL Server client tools, you can view intrinsic properties in SQL Server Management Studio (SSMS).  
  
 Intrinsic properties include `ID`, `KEY`, `KEYx`, and `NAME`, which are properties exposed by every member, at any level. You can also return positional information, such as `LEVEL_NUMBER` or `PARENT_UNIQUE_NAME`, among others.  
  
 Depending on how you construct the query, and on the client application you are using to execute queries, member properties may or may not be visible in the result set. If you are using SQL Server Management Studio to test or run queries, you can double-click a member in the result set to open the Member Properties dialog box, showing the values for each intrinsic member property.  
  
 For an introduction to using and viewing dimension member properties, see [Viewing SSAS Member Properties within an MDX Query Window in SSMS](https://go.microsoft.com/fwlink/?LinkId=317362).  
  
> [!NOTE]  
>  As a provider that is compliant with the OLAP section of the OLE DB specification dated March 1999 (2.6), [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] supports the intrinsic member properties listed in this topic.  
>   
>  Providers other than [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)][!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] may support additional intrinsic member properties. For more information about the intrinsic member properties supported by other providers, refer to the documentation that comes with those providers.  
  
## Types of Member Properties  
 The intrinsic member properties supported by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] are of two types:  
  
 Context sensitive member properties  
 These member properties must be used in the context of a specific hierarchy or level, and supply values for each member of the specified dimension or level.  
  
 Notice how the following example includes the path for the `KEY` property: `MEMBER [Measures].[Parent Member Key] AS [Product].[Product Categories].CurrentMember.Parent.PROPERTIES("KEY")`.  
  
 Non-context sensitive member properties  
 These member properties cannot be used in the context of a specific dimension or level, and return values for all members on an axis.  
  
 Context-insensitive properties standalone and do not include path information. Notice how there is no dimension or level specified for `PARENT_UNIQUE_NAME` in the following example: `DIMENSION PROPERTIES PARENT_UNIQUE_NAME ON COLUMNS`  
  
 Regardless of whether the intrinsic member property is context sensitive or not, the following usage rules apply:  
  
-   You can specify only those intrinsic member properties that relate to dimension members that are projected on the axis.  
  
-   You can mix requests for context sensitive member properties in the same query with non-context sensitive intrinsic member properties.  
  
-   You use the `PROPERTIES` keyword to query for the properties.  
  
 The following sections describe both the various context sensitive and non-context sensitive intrinsic member properties available in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], and how to use the `PROPERTIES` keyword with each type of property.  
  
## Context Sensitive Member Properties  
 All dimension members and level members support a list of intrinsic member properties that are context sensitive. The following table lists these context-sensitive properties.  
  
|Property|Description|  
|--------------|-----------------|  
|`ID`|The internally maintained ID for the member.|  
|`Key`|The value of the member key in the original data type. MEMBER_KEY is for backward-compatibility.  MEMBER_KEY has the same value as KEY0 for non-composite keys, and MEMBER_KEY property is null for composite keys.|  
|`KEYx`|The key for the member, where x is the zero-based ordinal of the key. KEY0 is available for composite and non-composite keys, but primarily used for composite keys.<br /><br /> For composite keys, KEY0, KEY1, KEY2, and so on, collectively form the composite key. You can use each one independently in a query to return that portion of the composite key. For example, specifying KEY0 returns the first part of the composite key, specifying KEY1 returns the next part of the composite key, and so on.<br /><br /> If the key is non-composite, then KEY0 is equivalent to `Key`.<br /><br /> Note that `KEYx` can be used in context as well as without context. For this reason, it appears in both lists.<br /><br /> For an example of how to use this member property, see [A Simple MDX Tidbit: Key0, Key1, Key2](https://go.microsoft.com/fwlink/?LinkId=317364).|  
|`Name`|The name of the member.|  
  
### PROPERTIES Syntax for Context Sensitive Properties  
 You use these member properties in the context of a specific dimension or level, and supply values for each member of the specified dimension or level.  
  
 For dimension member properties, you precede the name of the property with the name of the dimension to which the property applies. The following example shows the appropriate syntax:  
  
 `DIMENSION PROPERTIES Dimension.Property_name`  
  
 For a level member property, you can precede the name of the property with just the level name or, for additional specification, both the dimension and level name. The following example shows the appropriate syntax:  
  
 `DIMENSION PROPERTIES [Dimension.]Level.Property_name`  
  
 To illustrate, you would like to return all the names of each referenced member in the `[Sales]` dimension. To return these names, you would use the following statement in a Multidimensional Expressions (MDX) query:  
  
 `DIMENSION PROPERTIES [Sales].Name`  
  
## Non-Context Sensitive Member Properties  
 All members support a list of intrinsic member properties that are the same regardless of context. These properties provide additional information that can be used by applications to enhance the user's experience.  
  
 The following table lists the non-context sensitive intrinsic properties supported by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
> [!NOTE]  
>  Columns in the MEMBERS schema rowset support the intrinsic member properties listed in the following table. For more information about the `MEMBERS` schema rowset, see [MDSCHEMA_MEMBERS Rowset](https://docs.microsoft.com/bi-reference/schema-rowsets/ole-db-olap/mdschema-members-rowset).  
  
|Property|Description|  
|--------------|-----------------|  
|`CATALOG_NAME`|The name of the cube to which this member belongs.|  
|`CHILDREN_CARDINALITY`|The number of children that the member has. This can be an estimate, so you should not rely on this to be the exact count. Providers should return the best estimate possible.|  
|`CUSTOM_ROLLUP`|The custom member expression.|  
|`CUSTOM_ROLLUP_PROPERTIES`|The custom member properties.|  
|`DESCRIPTION`|A human-readable description of the member.|  
|`DIMENSION_UNIQUE_NAME`|The unique name of the dimension to which this member belongs. For providers that generate unique names by qualification, each component of this name is delimited.|  
|`HIERARCHY_UNIQUE_NAME`|The unique name of the hierarchy. If the member belongs to more than one hierarchy, there is one row for each hierarchy to which the member belongs. For providers that generate unique names by qualification, each component of this name is delimited.|  
|`IS_DATAMEMBER`|A Boolean that indicates whether the member is a data member.|  
|`IS_PLACEHOLDERMEMBER`|A Boolean that indicates whether the member is a placeholder.|  
|`KEYx`|The key for the member, where x is the zero-based ordinal of the key. KEY0 is available for composite and non-composite keys.<br /><br /> If the key is non-composite, then KEY0 is equivalent to `Key`.<br /><br /> For composite keys, KEY0, KEY1, KEY2, and so on, collectively form the composite key. You can reference each one independently in a query to return that portion of the composite key. For example, specifying KEY0 returns the first part of the composite key, specifying KEY1 returns the next part of the composite key, and so on.<br /><br /> Note that `KEYx` can be used in context as well as without context. For this reason, it appears in both lists.<br /><br /> For an example of how to use this member property, see [A Simple MDX Tidbit: Key0, Key1, Key2](https://go.microsoft.com/fwlink/?LinkId=317364).|  
|`LCID` *x*|The translation of the member caption in the locale ID hexadecimal value, where *x* is the locale ID decimal value (for example, LCID1009 as English-Canada). This is only available if the translation has the caption column bound to the data source.|  
|`LEVEL_NUMBER`|The distance of the member from the root of the hierarchy. The root level is zero.|  
|`LEVEL_UNIQUE_NAME`|The unique name of the level to which the member belongs. For providers that generate unique names by qualification, each component of this name is delimited.|  
|`MEMBER_CAPTION`|A label or caption associated with the member. The caption is primarily for display purposes. If a caption does not exist, the query returns `MEMBER_NAME`.|  
|`MEMBER_KEY`|The value of the member key in the original data type. MEMBER_KEY is for backward-compatibility.  MEMBER_KEY has the same value as KEY0 for non-composite keys, and MEMBER_KEY property is null for composite keys.|  
|`MEMBER_NAME`|The name of the member.|  
|`MEMBER_TYPE`|The type of the member. This property can have one of the following values: <br />**MDMEMBER_TYPE_REGULAR**<br />**MDMEMBER_TYPE_ALL**<br />**MDMEMBER_TYPE_FORMULA**<br />**MDMEMBER_TYPE_MEASURE**<br />**MDMEMBER_TYPE_UNKNOWN**<br /><br /> <br /><br /> MDMEMBER_TYPE_FORMULA takes precedence over MDMEMBER_TYPE_MEASURE. Therefore, if there is a formula (calculated) member on the Measures dimension, the `MEMBER_TYPE` property for the calculated member is MDMEMBER_TYPE_FORMULA.|  
|`MEMBER_UNIQUE_NAME`|The unique name of the member. For providers that generate unique names by qualification, each component of this name is delimited.|  
|`MEMBER_VALUE`|The value of the member in the original type.|  
|`PARENT_COUNT`|The number of parents that this member has.|  
|`PARENT_LEVEL`|The distance of the member's parent from the root level of the hierarchy. The root level is zero.|  
|`PARENT_UNIQUE_NAME`|The unique name of the member's parent. `NULL` is returned for any members at the root level. For providers that generate unique names by qualification, each component of this name is delimited.|  
|`SKIPPED_LEVELS`|The number of skipped levels for the member.|  
|`UNARY_OPERATOR`|The unary operator for the member.|  
|`UNIQUE_NAME`|The fully-qualified name of the member, in this format: [dimension].[level].[key6.]|  
  
### PROPERTIES Syntax for Non-Context Sensitive Properties  
 Use the following syntax to specify an intrinsic, non-context sensitive member property using the `PROPERTIES` keyword:  
  
 `DIMENSION PROPERTIES Property`  
  
 Notice that this syntax does not allow the property to be qualified by a dimension or level. The property cannot be qualified because an intrinsic member property that is not context sensitive applies to all members of an axis.  
  
 For example, an MDX statement that specifies the `DESCRIPTION` intrinsic member property would have the following syntax:  
  
 `DIMENSION PROPERTIES DESCRIPTION`  
  
 This statement returns the description of each member in the axis dimension. If you tried to qualify the property with a dimension or level, as in *Dimension*`.DESCRIPTION` or *Level*`.DESCRIPTION`, the statement would not validate.  
  
### Example  
 The following examples show MDX queries that return intrinsic properties.  
  
 **Example 1: Use context-sensitive intrinsic properties in query**  
  
 The following example returns parent ID, key, and name for each product category. Notice how the properties are exposed as measures. This lets you view the properties in a cellset when you run the query, rather than the Member Properties dialog in SSMS. You might run a query like this to retrieve member metadata from a cube that is already deployed.  
  
```  
WITH  
MEMBER [Measures].[Parent Member ID] AS  
 [Product].[Product Categories].CurrentMember.Parent.PROPERTIES("ID")  
MEMBER [Measures].[Parent Member Key] AS  
 [Product].[Product Categories].CurrentMember.Parent.PROPERTIES("KEY")  
MEMBER [Measures].[Parent Member Name] AS  
 [Product].[Product Categories].CurrentMember.Parent.PROPERTIES("Name")  
SELECT  
 {[Measures].[Parent Member ID], [Measures].[Parent Member Key], [Measures].[Parent Member Name] } on COLUMNS,   
 [Product].[Product Categories].AllMembers on ROWS  
FROM [Adventure Works]  
```  
  
 **Example 2: Non-context-sensitive intrinsic properties**  
  
 The following example is the full list of non-context-sensitive intrinsic properties. After running the query in SSMS, click individual members to view properties in the Member Properties dialog box.  
  
```  
SELECT [Measures].[Sales Amount Quota] on COLUMNS,  
[Employee].[Employees].members  
DIMENSION PROPERTIES  
 CATALOG_NAME ,   
 CHILDREN_CARDINALITY ,  
 CUSTOM_ROLLUP ,   
 CUSTOM_ROLLUP_PROPERTIES ,   
 DESCRIPTION ,   
 DIMENSION_UNIQUE_NAME ,   
 HIERARCHY_UNIQUE_NAME ,  
 IS_DATAMEMBER ,   
 IS_PLACEHOLDERMEMBER ,   
 KEY0 ,  
 LCID ,  
 LEVEL_NUMBER ,  
 LEVEL_UNIQUE_NAME ,  
 MEMBER_CAPTION ,   
 MEMBER_KEY ,   
 MEMBER_NAME ,   
 MEMBER_TYPE ,  
 MEMBER_UNIQUE_NAME ,   
 MEMBER_VALUE ,   
 PARENT_COUNT ,   
 PARENT_LEVEL ,   
 PARENT_UNIQUE_NAME ,  
 SKIPPED_LEVELS ,   
 UNARY_OPERATOR ,   
 UNIQUE_NAME    
 ON ROWS  
FROM [Adventure Works]  
WHERE [Employee].[Employee Department].[Department].&[Sales]  
```  
  
 **Example 3: Return member properties as data in a result set**  
  
 The following example returns the translated caption for the product category member in the Product dimension in the Adventure Works cube for specified locales.  
  
```  
WITH   
MEMBER Measures.CategoryCaption AS Product.Category.CurrentMember.MEMBER_CAPTION  
MEMBER Measures.SpanishCategoryCaption AS Product.Category.CurrentMember.Properties("LCID3082")  
MEMBER Measures.FrenchCategoryCaption AS Product.Category.CurrentMember.Properties("LCID1036")  
SELECT   
{ Measures.CategoryCaption, Measures.SpanishCategoryCaption, Measures.FrenchCategoryCaption } ON 0  
,[Product].[Category].MEMBERS ON 1  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [PeriodsToDate &#40;MDX&#41;](/sql/mdx/periodstodate-mdx)   
 [Children &#40;MDX&#41;](/sql/mdx/children-mdx)   
 [Hierarchize &#40;MDX&#41;](/sql/mdx/hierarchize-mdx)   
 [Count &#40;Set&#41; &#40;MDX&#41;](/sql/mdx/count-set-mdx)   
 [Filter &#40;MDX&#41;](/sql/mdx/filter-mdx)   
 [AddCalculatedMembers &#40;MDX&#41;](/sql/mdx/addcalculatedmembers-mdx)   
 [DrilldownLevel &#40;MDX&#41;](/sql/mdx/drilldownlevel-mdx)   
 [Properties &#40;MDX&#41;](/sql/mdx/properties-mdx)   
 [PrevMember &#40;MDX&#41;](/sql/mdx/prevmember-mdx)   
 [Using Member Properties &#40;MDX&#41;](mdx-member-properties.md)   
 [MDX Function Reference &#40;MDX&#41;](/sql/mdx/mdx-function-reference-mdx)  
  
  
