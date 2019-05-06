---
title: "Extended Field Properties for an Analysis Services Database (SSRS) | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: conceptual
ms.assetid: 1d7d87e2-bf0d-4ebb-a287-80b5a967a3f2
author: maggiesMSFT
ms.author: maggies
---
# Extended Field Properties for an Analysis Services Database (SSRS)
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data processing extension supports extended field properties. Extended field properties are properties in addition to the field properties **Value** and **IsMissing** that are available on the data source and supported by the data processing extension. Extended properties do not appear in the Report Data pane as part of the field collection for a report dataset. You can include extended field property values in your report by writing expressions that specify them by name using the built-in **Fields** collection.  
  
 Extended properties include predefined properties and custom properties. Predefined properties are properties common to multiple data sources that are mapped to specific field property names and can be accessed through the built-in **Fields** collection by name. Custom properties are specific to each data provider and can be accessed through the built-in **Fields** collection only through syntax using the extended property name as a string.  
  
 When you use the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] MDX query designer in graphical mode to define your query, a predefined set of cell properties and dimension properties are automatically added to the MDX query. You can only use extended properties that are specifically listed in the MDX query in your report. Depending on your report, you may want to modify the default MDX command text to include other dimension or custom properties defined in the cube. For more information about extended fields available in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data sources, see [Creating and Using Property Values &#40;MDX&#41;](https://msdn.microsoft.com/library/0cafb269-03c8-4183-b6e9-220f071e4ef2).  
  
## Working with Field Properties in a Report  
 Extended field properties include predefined properties and data provider-specific properties. Field properties do not appear with the field list in the **Report Data** pane, even though they are in the query built for a dataset; therefore, you cannot drag field properties onto your report design surface. Instead, you must drag the field onto the report and then change the **Value** property of the field to the property that you want to use. For example, if the cell data from a cube has already been formatted, you can use the FormattedValue field property by using the following expression: `=Fields!FieldName.FormattedValue`.  
  
 To refer to an extended property that is not predefined, use the following syntax in an expression:  
  
-   *Fields!FieldName("PropertyName")*  
  
## Predefined Field Properties  
 In most cases, predefined field properties apply to measures, levels, or dimensions. A predefined field property must have a corresponding value stored in the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data source. If a value does not exist, or if you specify a measure-only field property on a level (for example), the property returns a null value.  
  
 You can use either of the following syntaxes to refer to a predefined property from an expression:  
  
-   *Fields!FieldName.PropertyName*  
  
-   *Fields!FieldName("PropertyName")*  
  
 The following table provides a list of predefined field properties that you can use.  
  
|**Property**|**Type**|**Description or expected value**|  
|------------------|--------------|---------------------------------------|  
|**Value**|**Object**|Specifies the data value of the field.|  
|**IsMissing**|**Boolean**|Indicates whether the field was found in the resulting data set.|  
|**UniqueName**|**String**|Returns the fully qualified name of a level. For example, the **UniqueName** value for an employee might be *[Employee].[Employee Department].[Department].&[Sales].&[North American Sales Manager].&[272]*.|  
|**BackgroundColor**|**String**|Returns the background color defined in the database for the field.|  
|**Color**|**String**|Returns the foreground color defined in the database for the item.|  
|**FontFamily**|**String**|Returns the name of the font defined in the database for the item.|  
|**FontSize**|**String**|Returns the point size of the font defined in the database for the item.|  
|**FontWeight**|**String**|Returns the weight of the font defined in the database for the item.|  
|**FontStyle**|**String**|Returns the style of the font defined in the database for the item.|  
|**TextDecoration**|**String**|Returns special text formatting defined in the database for the item.|  
|**FormattedValue**|**String**|Returns a formatted value for a measure or key figure. For example, the **FormattedValue** property for **Sales Amount Quota** returns a currency format like $1,124,400.00.|  
|**Key**|**Object**|Returns the key for a level.|  
|**LevelNumber**|**Integer**|For parent-child hierarchies, returns the level or dimension number.|  
|**ParentUniqueName**|**String**|For parent-child hierarchies, returns a fully qualified name of the parent level.|  
  
> [!NOTE]  
>  Values exist for these extended field properties only if the data source (for example, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] cube) provides these values when your report runs and retrieves the data for its datasets. You can then refer to those field property values from any expression using the syntax described in the following section. However, because these fields are specific to this data provider, changes that you make to these values are not saved with the report definition.  
  
### Example Extended Properties  
 To illustrate extended properties, the following MDX query and result set include several member properties available from a dimension attribute defined for a cube. The member properties included are MEMBER_CAPTION, UNIQUENAME, Properties("Day Name"), MEMBER_VALUE, PARENT_UNIQUE_NAME, and MEMBER_KEY.  
  
 This MDX query runs against the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] cube in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] DW database, included with the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample databases.  
  
```  
WITH MEMBER [Measures].[DateCaption]   
      AS '[Date].[Date].CURRENTMEMBER.MEMBER_CAPTION'   
   MEMBER [Measures].[DateUniqueName]   
      AS '[Date].[Date].CURRENTMEMBER.UNIQUENAME'   
   MEMBER [Measures].[DateDayName]   
      AS '[Date].[Date].Properties("Day Name")'   
   MEMBER [Measures].[DateValueinOriginalDatatype]   
      AS '[Date].[Date].CURRENTMEMBER.MEMBER_VALUE'   
   MEMBER [Measures].[DateParentUniqueName]   
      AS '[Date].[Date].CURRENTMEMBER.PARENT_UNIQUE_NAME'   
   MEMBER [Measures].[DateMemberKeyinOriginalDatatype]   
      AS '[Date].[Date].CURRENTMEMBER.MEMBER_KEY'   
SELECT {  
   [Measures].[DateCaption],   
   [Measures].[DateUniqueName],   
   [Measures].[DateDayName],   
   [Measures].[DateValueinOriginalDatatype],  
   [Measures].[DateParentUniqueName],  
   [Measures].[DateMemberKeyinOriginalDatatype]  
   } ON COLUMNS , [Date].[Date].ALLMEMBERS ON ROWS   
FROM [Adventure Works]  
  
```  
  
 When you run this query in an MDX query pane, you get a result set with 1158 rows. The first four rows are shown in the following table.  
  
|DateCaption|DateUniqueName|DateDayName|DateValueinOriginalDatatype|DateParentUniqueName|DateMemberKeyinOriginalDatatype|  
|-----------------|--------------------|-----------------|---------------------------------|--------------------------|-------------------------------------|  
|All Periods|[Date].[Date].[All Periods]|(null)|(null)|(null)|0|  
|1-Jul-01|[Date].[Date].&[1]|Sunday|7/1/2001|[Date].[Date].[All Periods]|1|  
|2-Jul-01|[Date].[Date].&[2]|Monday|7/2/2001|[Date].[Date].[All Periods]|2|  
|3-Jul-01|[Date].[Date].&[3]|Tuesday|7/3/2001|[Date].[Date].[All Periods]|3|  
  
 Default MDX queries built using the MDX Query Designer in graphical mode only include MEMBER_CAPTION and UNIQUENAME for dimension properties. By default, these values always are data type **String**.  
  
 If you need a member property in its original data type, you can include an additional property MEMBER_VALUE by modifying the default MDX statement in the text-based query designer. In the following simple MDX statement, MEMBER_VALUE has been added to the list of dimension properties to retrieve.  
  
```  
SELECT NON EMPTY {[Measures].[Order Count]} ON COLUMNS,   
NON EMPTY { ([Date].[Month of Year].[Month of Year] ) }   
DIMENSION PROPERTIES   
   MEMBER_CAPTION, MEMBER_UNIQUE_NAME, MEMBER_VALUE ON ROWS   
FROM [Adventure Works]  
CELL PROPERTIES   
   VALUE, BACK_COLOR, FORE_COLOR,   
   FORMATTED_VALUE, FORMAT_STRING,   
   FONT_NAME, FONT_SIZE, FONT_FLAGS  
```  
  
 The first four rows of the result in the MDX Results pane appear in the following table.  
  
|Month of Year|Order Count|  
|-------------------|-----------------|  
|January|2,481|  
|February|2,684|  
|March|2,749|  
|April|2,739|  
  
 Even though the properties are part of the MDX select statement, they do not appear in the result set columns. Nevertheless, the data is available for a report by using the extended properties feature. In an MDX query result pane in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], you can double-click on the cell and see the cell property values if they are set in the cube. If you double-click on the first Order Count cell that contains 1,379, you will see a pop-up window with the following cell properties:  
  
|Property|Value|  
|--------------|-----------|  
|CellOrdinal|0|  
|VALUE|2481|  
|BACK_COLOR|(null)|  
|FORE_COLOR|(null)|  
|FORMATTED_VALUE|2,481|  
|FORMAT_STRING|#,#|  
|FONT_NAME|(null)|  
|FONT_SIZE|(null)|  
|FONT_FLAGS|(null)|  
  
 If you create a report dataset with this query and bind the dataset to a table, you can see the default VALUE property for a field, for example, `=Fields!Month_of_Year!Value`. If you set this expression as the sort expression for the table, your results will be to sort the table alphabetically by month because the Value field uses a **String** data type. To sort the table in so that the months are in the order they occur in the year with January first and December last, use the following expression:  
  
```  
=Fields!Month_of_Year("MEMBER_VALUE")  
```  
  
 This sorts the value of the field in its original integer data type from the data source.  
  
## See Also  
 [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)   
 [Built-in Collections in Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/built-in-collections-in-expressions-report-builder.md)   
 [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/dataset-fields-collection-report-builder-and-ssrs.md)  
  
  
