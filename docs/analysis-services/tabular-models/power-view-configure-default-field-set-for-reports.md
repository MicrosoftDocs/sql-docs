---
title: "Configure default field set for Power View reports in Analysis Services | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Power View - Configure Default Field Set for Reports
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  A default field set is a predefined list of columns and measures that are automatically added to a [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] report canvas when the table is selected in the report field list. Tabular model authors can create a default field set to eliminate redundant steps for report authors who use the model for their reports. For example, if you know that most report authors who work with customer contact information always want to see a contact name, a primary phone number, an email address, and a company name, you can pre-select those columns so that they are always added to the report canvas when the author clicks the Customer Contact table.  
  
> [!NOTE]  
>  A default field set applies only to a tabular model used as a data model in [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)]. Default field sets are not supported in Excel pivot reports.  
  
## Creating a Default Field Set  
 You can determine which fields, if any, are included by default whenever a specific table is selected in [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)]. You can also determine the order in which fields appear in the list. To specify a default field set, you set report properties in the tabular model project.  
  
#### To add a default field set  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click the table (tab) for which you are configuring a default field list.  
  
2.  In the **Properties** window, in the **Default Field Set** property, click **Click to edit**.  
  
3.  In the Default Field Set dialog, select one or more fields. You can choose any field in the table, including measures. Hold down the Shift key to select a range, or the Ctrl key to select individual fields.  
  
4.  Click **Add** to add them to the default field set.  
  
5.  Use the Up and Down buttons to specify an order to the field list. Fields will be added to the report in the order defined for the field set.  
  
6.  Repeat these steps for other tables in your workbook.  
  
## Next Step  
 After you create a default field set, you can further influence the report design experience by specifying default labels, default images, default group behavior, or whether rows that contain the same value are grouped together in one row or listed individually. For more information, see [Configure Table Behavior Properties for Power View Reports](../../analysis-services/tabular-models/power-view-configure-table-behavior-properties-for-reports.md).  
  
  
