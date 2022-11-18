---
description: "MDX Data Definition - CREATE ACTION"
title: "CREATE ACTION Statement (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# MDX Data Definition - CREATE ACTION


  Creates an action that can be associated with a cube, dimension, hierarchy, or subordinate object.  
  
## Syntax  
  
```  
  
CREATE ACTION CURRENTCUBE | Cube_Name  
   .Action_Name <action body>  
<action body> ::=   
FOR   
        CUBE   
    | Hierarchy_Name [MEMBERS]   
    | Level_Name [MEMBERS]   
    | CELLS   
    | SET }   
      AS 'MDX_Expression'   
        [, TYPE = '  
              { URL   
            | HTML   
            | STATEMENT   
               | DATASET   
            | ROWSET   
            | COMMANDLINE   
               | PROPRIETARY }   
         ']  
   [ , INVOCATION = 'INTERACTIVE | ON_OPEN | BATCH ' ]  
   [ , APPLICATION = String_Expression ]  
   [ , DESCRIPTION = String_Expression ]  
   [ , CAPTION = 'MDX_Expression' ]  
```  
  
## Arguments  
 *Cube_Name*  
 A valid string that provides a cube name.  
  
 *Action_ Name*  
 A valid string that provides the name of the action being created.  
  
 *Hierarchy_ Name*  
 A valid string that provides a hierarchy name.  
  
 *Level_ Name*  
 A valid string that provides a level name.  
  
 *Member_ Name*  
 A valid string that provides a member name or member key.  
  
 *MDX_Expression*  
 A valid MDX expression.  
  
 *String_Expression*  
 A valid string expression.  
  
## Remarks  
 It is possible for client applications to create and run actions that are unsafe; it is also possible for client applications to use unsafe functions. To avoid these situations, use the **Safety Options** property. For more information, see Safety Options Property.  
  
> [!NOTE]  
>  This statement is included for backwards compatibility. Actions new to [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], such as Drillthrough or Report actions, are not supported.  
  
## Action Types  
 The following table describes the different types of actions available in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
|Action type|Description|  
|-----------------|-----------------|  
|**URL**|The returned action string is a URL that should be opened using an Internet browser.<br /><br /> Note: If this action does not start with `https://` or `https://`, the action will be unavailable to the browser unless **SafetyOptions** is set to **DBPROPVAL_MSMD_SAFETY_OPTIONS_ALLOW_ALL**.|  
|**HTML**|The returned action string is an HTML script. The string should be saved to a file and the file should be rendered using an Internet browser. In this case, a whole script may be run as part of the generated HTML.|  
|**STATEMENT**|The returned action string is a statement that needs to be executed by setting the **ICommand::SetText** method of a command object to the string and calling the **ICommand::Execute**method. If the command does not succeed, an error is returned.|  
|**DATASET**|The returned action string is an MDX statement that needs to be run by setting the **ICommand::SetText** method of a command object to the string and calling the **ICommand::Execute** method. The requested interface ID (IID) should be **IDataset**. The command succeeds if a data set has been created. The client application should allow the user to browse the returned data set.|  
|**ROWSET**|Similar to **DATASET**, but instead of requesting an IID of **IDataset**, the client application should ask for an IID of **IRowset**. The command succeeds if a rowset has been created. The client application should allow the user to browse the returned rowset.|  
|**COMMANDLINE**|The client application should execute the action string. The string is a command line.|  
|**PROPRIETARY**|A client application should not display, nor execute the action unless the application has a custom, nongeneric knowledge of the specific action. Proprietary actions are not returned to the client application unless the client application explicitly asks for these by setting the appropriate restriction on the **APPLICATION_NAME**.|  
  
## Invocation Types  
 The following table describes the different types of invocations available in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. The invocation type is used only by the client application to help determine when to invoke the action. The invocation type does not actually determine the invocation behavior of the action.  
  
|Invocation type|Description|  
|---------------------|-----------------|  
|**INTERACTIVE**|The action should be invoked by the client application through user interaction.|  
|**ON_OPEN**|The action should be invoked by the client application when the target object is opened. This invocation type is not currently implemented.|  
|**BATCH**|The action should be invoked by the client application when the target object is involved in a batch operation, as determined by the client application. This invocation type is not currently implemented.|  
  
### Scope  
 Each action is defined for a specific cube and has a unique name in that cube. An action can have one of the scopes listed in the following table.  
  
 Cube scope  
 For actions independent of specific dimensions, members, or cells; for example: "Launch terminal emulation for AS/400 production system".  
  
 Dimension scope  
 The action applies to a specific dimension. These actions are not dependent on specific selection of levels or members.  
  
 Level scope  
 The action applies to a specific dimension level. These actions are not dependent on specific selection of a member in that dimension.  
  
 Member scope  
 The action applies to specific level members.  
  
 Cell scope  
 The action applies to specific cells only.  
  
 Set scope  
 The action applies to a set only. The name, **ActionParameterSet**, is reserved for use by the application inside the expression of the action.  
  
## See Also  
 [MDX Data Definition Statements &#40;MDX&#41;](../mdx/mdx-data-definition-statements-mdx.md)  
  
  
