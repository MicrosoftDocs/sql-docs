---
title: "Create a Cube from a template without using a Data Source View | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 5c8c09b1-140c-48db-9b9f-d18a051d7dbd
author: minewiskan
ms.author: owend
manager: craigg
---
# Create a Cube from a template without using a Data Source View
  Select **Build the cube without using a data source** on the first page of the Cube Wizard to create a cube without using a data source view. You can later use the Schema Generation Wizard to generate the relational schema for the data source view based on the structure of the cube and possibly other [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects. For more information about generating a schema, see [Schema Generation Wizard &#40;Analysis Services&#41;](schema-generation-wizard-analysis-services.md).  
  
## Selecting the Build Method  
 In the Cube Wizard, on the **Select Build Method** page, click **Build the cube without using a data source**. To build the cube by using an existing cube template, select the **Use a cube template** check box. . If you do not select to use a template, you must set the options manually.  
  
 Cube templates contain predefined measures, measure groups, dimensions, hierarchies, and attributes. If you select a template, the wizard uses the object definitions in the templates as the basis for setting options in the following pages. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is installed with several templates for standard cubes. The server administrator can also add cube or dimension templates that are designed specifically for your organization's data.  
  
## Selecting Dimensions  
 Use the **Select Dimensions** page of the wizard to add existing dimensions to the cube. This page appears only if there are already shared dimensions without a data source in the project or database. It does not list dimensions that have a data source.  
  
 To add existing dimensions, select one or more dimensions in the **Shared dimensions** list and click the right arrow (**>**) button to move them to the **Cube dimensions** list. Click the double-arrow (**>>**) button to move all the dimensions in the list.  
  
## Defining New Measures  
 Use the **Define New Measures** page of the wizard to specify the measures and measure groups in the new cube. The measure groups you specify here will correspond to fact tables in the generated schema. The measures you specify here will correspond to numeric non-key columns in the tables.  
  
 If you use a template to create the cube, measures in the template are listed in grid format under **Select measures from template**. The check box next to every measure in the list is initially selected. You can clear the check box next to any measure that you do not want to include in the cube. To add or remove all the measures in the list, select or clear the check box on the title bar of the grid.  
  
 You can add measures to the cube in the list under **Add new measures**. To add a new measure, click the first empty cell in the **Measure Name** column (which displays **Add new measure**). Specify a measure name, measure group, data type, and aggregation for each new measure. To delete a measure from the **Add new measures** list, click the delete icon (**X**). If you do not use a template, **Add new measures** is the only list on this page of the wizard.  
  
 Both the **Select measures from template** grid and the **Add new measures** grid display values under the columns described in the following table. You can click a value in either list to change it.  
  
|Column|Description|  
|------------|-----------------|  
|**Measure Name**|A value in this column defines the name of a measure in the cube. Click a value in this column to type a name. Click **Add new measure** in this column to create a new measure. This column sets the `Name` property on the measure object.|  
|**Measure Group**|The name of the measure group that contains the measure. Click this value to either choose or type a name. If you delete all the measures that belong to a particular measure group, the measure group is removed as well. This column sets the `Name` property for the measure group object.|  
|**Data Type**|The data type for the measure. Click this value to change the data type. The default when you create a measure is `Single`. This column sets the `DataType` property on the measure object.|  
|**Aggregation**|The standard aggregation for the measure. Click this cell to specify one of the standard aggregations for the measure (or **None**). The default when you create a measure is `Sum`. This column sets the `AggregationFunction` property on the measure object.|  
  
## Defining New Dimensions  
 Use the **Define New Dimensions** page of the wizard to specify the dimensions in the new cube.  
  
 If you use a template to create the cube, the grid under **Select dimensions from template** displays the dimensions in the template. You can clear the check box next to any dimension to remove it from the cube. Clear the check box on the title bar of the grid to remove all the dimensions listed. If you do not use a template, this grid lists only the Time dimension.  
  
 You can add dimensions to the cube in the grid under **Add new dimensions**. To add a dimension, click the cell in the `Name` column that contains the text **Add new dimension**, and then type a name for the dimension. To remove a row from the list, click the delete icon (**X**).  
  
 Both the **Select dimensions from template** grid and the **Add new dimensions** grid display values under the columns described in the following table. You can click a value in either list to change it.  
  
|Column|Description|  
|------------|-----------------|  
|**Type**|Displays the dimension type for a template dimension. Click this cell to change the dimension type for a dimension. This column sets the **Type** property for the dimension object.|  
|`Name`|Displays the dimension name. Click this cell to type a different name. This value sets the `Name` property for the dimension object.|  
|**SCD**|Specifies that this is a slowly changing dimension (SCD). Selecting this check box adds the SCD Start Date, End Date Original ID, and Status attributes to the dimension. **SCD** is selected by default if you use a template to create the cube and the wizard detects these four attribute types in a template dimension.|  
|**Attributes**|Displays the attributes that are to be created for the dimension. Each attribute name in the list is preceded by the dimension name. This list is read-only. You can edit the attributes by using Dimension Designer after you complete the wizard.|  
  
## Defining Time Periods  
 Use the **Define Time Periods** page of the wizard to specify the range of dates you want to include in the dimension. For example, you might choose a range starting on January 1 of the earliest year in your data and extending years past your most current transaction. Transactions that are outside the range either do not appear or appear as unknown members in the dimension, depending on the `UnknownMemberVisible` property setting for the dimension. The `UnknownMemberName` property specifies the caption for the unknown member. You can also change the first day of the week used by your data (the default is Sunday).  
  
> [!NOTE]  
>  The **Define Time Periods** page appears only if you include a time dimension in your cube on the **Define New Dimensions** page of the wizard.  
  
 Select the time periods (**Year**, **Half Year**, **Quarter**, **Trimester**, **Month**, **Ten Days**, **Week**, and **Date**) that you want to include in your schema. You must select the Date time period; the Date attribute is the key attribute for the dimension, so the dimension cannot function without it. You can also change the language used to label the members of the dimension.  
  
 The time periods you select create corresponding time attributes in the new time dimension. The wizard also adds related attributes that do not appear in the list. For example, when you select **Year** and **Half Year** time intervals, the wizard creates Day of Year, Day of Half Year, and Half Years of Year attributes, in addition to Year and Half Year attributes.  
  
 After you finish creating the cube, you can use Dimension Designer to add or remove time attributes. Because the Date attribute is the key attribute for the dimension, you cannot remove it. To hide the Date attribute from users, you can change the `AttributeHierarchyVisible` property to `False`.  
  
 All the available time periods appear in the Time Periods pane of Dimension Designer. (This pane replaces the **Data Source View** pane for dimensions that are based on dimension tables.) You can change the range of dates for a dimension by changing the **Source** (time binding) property setting for the dimension. Because this is a structural change, you must reprocess the dimension and any cubes that use it before browsing the data.  
  
## Specifying Additional Calendars  
 On the **Specify Additional Calendars** page of the wizard, select calendars on which to base hierarchies in the dimension. You can choose any of the following calendars.  
  
|Calendar|Description|  
|--------------|-----------------|  
|Fiscal calendar|A twelve-month fiscal calendar. If you select this calendar, specify the starting day and month for the fiscal year used by your organization.|  
|Reporting (or marketing) calendar|A twelve-month reporting calendar that includes two months of four weeks and one month of five weeks in a recurring three-month (quarterly) pattern. If you select this calendar, specify the starting day and month and the three-month pattern of 4-4-5, 4-5-4, or 5-4-4 weeks, where each digit represents the number of weeks in a month.|  
|Manufacturing calendar|A calendar that uses 13 periods of four weeks, divided into three quarters of four periods and one quarter of five periods. If you select this calendar, specify the starting week (between 1 and 4) and month for the manufacturing year, and the quarter with extra periods.|  
|ISO 8601 Calendar|The International Organization for Standardization (ISO) Representation of Dates and Time standard calendar (8601). This calendar has an integral number of 7-day weeks. To avoid splitting a week, this calendar starts a new year up to several days before or after January 1.|  
  
 The calendar and settings you select determine the attributes that are created in the dimension. For example, if you select the **Year** and **Quarter** time periods on the **Define Time Periods** page of the wizard and the **Fiscal calendar** on this page, the FiscalYear, FiscalQuarter, FiscalQuarterOfYear attributes are created for the fiscal calendar.  
  
 The wizard also creates calendar-specific hierarchies composed of the attributes that are created for the calendar. For every calendar, each level in every hierarchy rolls up into the level above it. For example, in the standard 12-month calendar, the wizard creates a hierarchy of Years and Weeks or Years and Months. However, weeks are not contained evenly within months in a standard calendar, so there is no hierarchy of Years, Months, and Weeks. In contrast, weeks in a reporting or manufacturing calendar are evenly divided into months, so in these calendars weeks do roll up into months.  
  
## Defining Dimension Usage  
 Use the **Define Dimension Usage** page of the wizard to specify which cube measures are aggregated by each dimension in the wizard. The **Dimension usage** grid on this page lists the dimensions as rows and measure groups as columns. Select the check box for any dimension and measure group combination in which the dimension aggregates the measures of that measure group.  
  
## Completing the Cube Wizard  
 On the **Completing the Wizard** page, review the structure of the new cube and in the **Cube name** box, type a name for it. Optionally, select the **Generate schema now** check box to start the schema generation wizard. In most cases, you should not select this check box if you plan to create additional objects. You can also use Cube Designer to generate the schema later.  
  
  
