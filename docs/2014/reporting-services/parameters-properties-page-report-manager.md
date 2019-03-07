---
title: "Parameters Properties Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: ebb53598-2378-46ae-8935-d5192f8ea49a
author: markingmyname
ms.author: maghan
manager: kfile
---
# Parameters Properties Page (Report Manager)
  Use the Parameters properties page to view or modify parameter settings for a parameterized report.  
  
 Parameters are specified in the report definition before the report is published. After the report is published, you can modify some parameter property values. The values that you can modify will vary based on how the parameters are defined in the report. For example, if a list of static values is defined for a parameter, you can choose a different static value to use as a default, but you cannot add or remove values from the list. Similarly, if the parameter is based on a query, all aspects of that query (including the dataset that is used, whether null or blank values are allowed, and whether a default value is provided) are defined in the report before publication.  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the Parameters properties page  
  
1.  Open Report Manager, and locate the report for which you want to modify parameter settings.  
  
2.  Hover over the report, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the General properties page for the report.  
  
4.  Select the **Parameters** tab. If the **Parameters** tab is not visible, the report does not contain parameters.  
  
## Options  
 **Parameter Name**  
 Specifies the name of the parameter.  
  
 **Data Type**  
 Specifies the data type of the parameter.  
  
 **Has Default**  
 Select this check box to specify whether the parameter has a default value. Selecting this check box enables **Default Value**. It also enables **Null** if the report parameter accepts null values. If **Has Default** is not selected, you must either hide the value or prompt the user to provide a value when the report runs.  
  
 **Default Value**  
 Specify a value for the parameter. To specify a default value, **Has Default** must be selected, and **Null** must not be selected. A default value may be provided through the report definition. If **Default Value** is populated with one or more static values, those values originate with the report. If **Default value** is **Query Based**, the parameter value is determined by a query that is defined in the report.  
  
 If **Default Value** accepts a value, you can type a constant or syntax that is valid for the data processing extension used with the report. For example, if the query language of the data processing extension supports wildcards, you can specify a wildcard character as a default value.  
  
 If you subsequently specify that a prompt be displayed to the user, the default value becomes an initial value that users can either use or modify. If you do not prompt for a parameter value, this value is used for all users who run the report.  
  
 **Null**  
 Select this check box to specify null as the default value. A null value means that the report runs even if the user does not provide a parameter value. If there is no check box in this column, the parameter does not accept null values.  
  
 **Hide**  
 Select this check box to hide the parameter in the parameter area that appears at the top of the report. The parameter will still appear in subscription definition pages and it can still be specified on a report URL. Hiding the parameter is useful when you want to always run the report with a default value that you specify.  
  
 Clear this check box if you want the parameter to be visible in the report.  
  
 **Prompt User**  
 Select this check box to display a text box that prompts users for a parameter value.  
  
 Clear this check box if you want to run the report in unattended mode (for example, to generate report history or report execution snapshots), if you want to use the same parameter value for all users, or if you do not require user input for the value.  
  
 **Display Text**  
 Provide a text string that appears next to the parameter text box. This string provides a label or descriptive text. There is no limit on string length. Longer text strings wrap within the space provided.  
  
## See Also  
 [General Properties Page, Reports &#40;Report Manager&#41;](../../2014/reporting-services/general-properties-page-reports-report-manager.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
