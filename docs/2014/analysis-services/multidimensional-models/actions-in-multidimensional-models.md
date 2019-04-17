---
title: "Actions in Multidimensional Models | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "actions [Analysis Services], creating"
  - "report actions [Analysis Services]"
  - "drillthrough actions [Analysis Services]"
  - "cubes [Analysis Services], actions"
ms.assetid: b9fee2b9-05a5-4077-848d-d8457326dc27
author: minewiskan
ms.author: owend
manager: craigg
---
# Actions in Multidimensional Models
  An action is an end user-initiated operation upon a selected cube or portion of a cube. The operation can start an application with the selected item as a parameter, or it can retrieve information about the selected item. For more information about actions, see [Actions &#40;Analysis Services - Multidimensional Data&#41;](actions-analysis-services-multidimensional-data.md).  
  
 Use the **Actions** tab of Cube Designer to build actions for a cube. Specify the following:  
  
 **Name**  
 Select a name that identifies the action.  
  
 **Action Target**  
 Select the object to which the action is attached. Generally, in client applications, the action is displayed when end users select the target object; however, the client application determines which end-user operation displays actions. For **Target type**, select from the following objects:  
  
-   Attribute members  
  
-   Cells  
  
-   Cube  
  
-   Dimension members  
  
-   Hierarchy  
  
-   Hierarchy members  
  
-   Level  
  
-   Level members  
  
 After you select the target object type, under **Target object**, select the cube object of the designated type.  
  
 **Condition (Optional)**  
 Specify an optional Multidimensional Expressions (MDX) expression that resolves to a Boolean value. If the value is `True`, the action is performed on the specified target. If the value is `False`, the action is not performed.  
  
 **Action Content**  
 Select the type of action. The following table summarizes the available types.  
  
|Type|Description|  
|----------|-----------------|  
|Data Set|Retrieves a dataset.|  
|Proprietary|Performs an operation by using an interface other than those listed in this table.|  
|Row Set|Retrieves a rowset.|  
|Statement|Runs an OLE DB command.|  
|URL|Displays a variable page in an Internet browser.|  
  
 For **Action Expression**, specify the parameters that are passed when the action is run. The syntax must evaluate to a string, and you must include an expression written in MDX. For example, your MDX expression can indicate a part of the cube that is included in the syntax. MDX expressions are evaluated before the parameters are passed. Also, MDX Builder is available to help you build MDX expressions.  
  
 **Additional Properties**  
 Select the property. The following table summarizes the available properties.  
  
|Property|Description|  
|--------------|-----------------|  
|**Invocation**|Specifies how the action is run. Interactive, the default, specifies that the action is run when a user accesses an object. The possible settings are:<br /><br /> Batch<br /><br /> Interactive<br /><br /> On Open|  
|**Application**|Describes the application of the action.|  
|**Description**|Describes the action.|  
|**Caption**|Provides a caption that is displayed for the action. If the caption is MDX, specify `True` for **Caption is MDX**.|  
|**Caption is MDX**|Specify `True` if the caption is MDX or `False` if it is not.|  
  
> [!NOTE]  
>  You must use Analysis Services Scripting Language (ASSL) or Analysis Management Objects (AMO) to define HTML and Command Line action types. For more information, see [Action Element &#40;ASSL&#41;](https://docs.microsoft.com/bi-reference/assl/objects/action-element-assl), [Type Element &#40;Action&#41; &#40;ASSL&#41;](https://docs.microsoft.com/bi-reference/assl/properties/type-element-action-assl), and [Programming AMO OLAP Advanced Objects](https://docs.microsoft.com/bi-reference/amo/programming-amo-olap-advanced-objects).  
  
## Creating a Reporting Action  
 The report server responds to URL-based requests for reports. To create a reporting action, on the **Cube** menu, click **New Reporting Action**. The following options are specific to a reporting action.  
  
 **Report Server**  
 The properties described in the following table are specified for the report server.  
  
|Property|Description|  
|--------------|-----------------|  
|**Server name**|The name of the computer running report server.|  
|**Server path**|The path exposed by report server.|  
|**Report format**|HTML5, HTML3, Excel, or PDF.|  
  
 **Parameters (Optional)**  
 The parameters are sent to the server as part of the URL string when the action is created. They include **Parameter Name** and **Parameter Value**, which is an MDX expression.  
  
 The report server URL is constructed as follows:  
  
```  
  
http://  
host  
/  
virtualdirectory  
/Path&  
parametername1  
=  
parametervalue1  
& ...  
```  
  
 For example:  
  
```  
http://localhost/ReportServer/Sales/YearlySalesByCategory?rs:Command=Render&Region=West  
```  
  
## Creating a Drillthrough Action  
 A drillthrough action is defined by a rowset action, which is returned to the client application as a drillthrough statement. The action target is a member of a measure group. To create a new drillthrough action, on the **Cube** menu, click **New Drillthrough Action**. The following options are specific to a drillthrough action:  
  
 **Drillthrough Columns**  
 Select one or more dimensions and, for each dimension, the drillthrough columns returned to the client application by the action.  
  
## See Also  
 [Cubes in Multidimensional Models](cubes-in-multidimensional-models.md)  
  
  
