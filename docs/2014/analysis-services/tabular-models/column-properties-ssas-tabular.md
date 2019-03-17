---
title: "Column Properties (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.bidtoolset.columnprop.f1"
ms.assetid: 4046c1a3-46c7-47db-b355-52e9c2f23671
author: minewiskan
ms.author: owend
manager: craigg
---
# Column Properties (SSAS Tabular)
  This topic describes tabular model column properties.  
  
 Sections in this topic:  
  
-   [Column Properties](#bkmk_properties)  
  
-   [To configure column property settings](#bkmk_config_prop)  
  
##  <a name="bkmk_properties"></a> Column Properties  
 **Basic**  
  
|Property|Default Setting|Description|  
|--------------|---------------------|-----------------|  
|**Column Name**||The name of the column as it is stored in the model and as it is displayed in a reporting client field list.|  
|**Data Format**|Automatically determined during import.|Specifies the display format to use for the data in this column. After you set a data format, you can set properties that are specific to each format. For example, if you choose the **Currency** format, you can set the number of visible decimal places, choose the thousands separator, and choose the currency symbol. This property has the following options:<br /><br /> **General**<br /><br /> **Decimal Number**<br /><br /> **Whole Number**<br /><br /> **Currency**<br /><br /> **Percentage**<br /><br /> **Scientific**<br /><br /> If the column values contain images, see **Representative Image**.|  
|**Data Type**|Automatically determined during import.|Specifies the data type for all values in the column.|  
|**Description**||A text description for the column.<br /><br /> In certain reporting clients, if an end-user places the cursor over this column in the field list, the description appears as a tooltip.|  
|**Hidden**|False|Specifies whether the column is hidden from reporting client field lists.<br /><br /> Set this property to **True** to hide this column in the display. For example, columns that contain identifiers or keys are typically not useful to the end user.<br /><br /> If you hide a column from the reporting client, the field is not suppressed in the model data. The field is still visible if you create a query against the model. A hidden column can still be used for grouping or sorting.<br /><br /> The **Hidden** property does not provide any form of data security. To secure data, use row filters in Roles. For more information, see [Roles &#40;SSAS Tabular&#41;](roles-ssas-tabular.md).|  
|**Sort By Column**||Specifies another column to sort the values in this column. A relationship must exist between the two columns.<br /><br /> This value must be the name of an existing column. You cannot specify a formula or measure.|  
  
 **Reporting Properties**  
  
 For detailed information on setting [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] table behavior properties, see [Configure Table Behavior Properties for Power View Reports &#40;SSAS Tabular&#41;](power-view-configure-table-behavior-properties-for-reports.md).  
  
|Property|Default Setting|Description|  
|--------------|---------------------|-----------------|  
|Default Image|False|Specifies which column provides an image that represents the row data (for example, a photo ID in an employee record).|  
|Default Label|False|Specifies which column provides a display name to represent row data (for example, employee name in an employee record).|  
|Image URL/Data Category (SP1)|False|Specifies the value in this column as a hyperlink to an image on a server. For example: http://localhost/images/image1.jpg.|  
|Keep Unique Rows|False|Specifies which columns provide values that should be treated as unique even if they are duplicates (for example, employee first name and last name, for cases where two or more employees share the same name).|  
|Row Identifier|False|Specifies a column that contains only unique values, allowing that column to be used as an internal grouping key.|  
|Summarize By|Default|Specifies reporting client tools apply the aggregate function SUM for column calculations when this column is added to a Field list. To change the default calculation, select it from the dropdown list. This property applies only to columns of type that can be aggregated.|  
|Table Detail Position|No Default Field Set|Specifies this column or measure can be added to a set of fields from a single table to enhance the table visualization experience in a reporting client.|  
  
###  <a name="bkmk_config_prop"></a> To configure column property settings  
  
1.  In the model designer, in a table, select a column.  
  
2.  In the **Properties** window, click on a property, and then type a value or click the down arrow to select a setting option.  
  
## See Also  
 [Power View Reporting Properties &#40;SSAS Tabular&#41;](properties-ssas-tabular.md)   
 [Hide or Freeze Columns &#40;SSAS Tabular&#41;](hide-or-freeze-columns-ssas-tabular.md)   
 [Add Columns to a Table &#40;SSAS Tabular&#41;](add-columns-to-a-table-ssas-tabular.md)  
  
  
