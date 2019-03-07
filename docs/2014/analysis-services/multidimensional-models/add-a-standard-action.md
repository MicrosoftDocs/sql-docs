---
title: "Add a Standard Action | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: ccb2928a-f75d-4acb-8ff8-fa80bb0935b2
author: minewiskan
ms.author: owend
manager: craigg
---
# Add a Standard Action
  You add an action to a database by using the Actions view in Cube Designer. That view can be accessed from [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. After you create an action, it becomes available to users after you reprocess the relevant cube. For more information, see [Processing Analysis Services Objects](processing-analysis-services-objects.md).  
  
### To create an action  
  
1.  Open the cube for which you want to create an action, and then click the **Actions** tab.  
  
2.  On the toolbar, click the **New Action** icon, and then in the expression pane, do the following:  
  
    -   In **Name**, type a name for the action.  
  
    -   From the **Target type** drop-down list, select the type of object to which you want to attach the action. The object you select in **Target Type** determines the objects that are available and the type of selection that you can make in **Target Object**. The following table lists valid **Target Object** selections for each target type.  
  
        |If you select the following target type|Make the following selection in Target Object|  
        |---------------------------------------------|---------------------------------------------------|  
        |Attribute Members|The only valid selection is a single attribute hierarchy. The target type of the action will be all members of the attribute wherever they appear (that is, the action will apply to user-defined hierarchies as well).|  
        |Cells|All cells is the only selection available. If you choose **Cells** as a target type, you can type an expression in **Condition** to restrict the cells with which the action is associated.|  
        |Cube|CURRENTCUBE is the only selection available. The action is associated with the current cube.|  
        |Dimension members|Select a single dimension. The action will be associated with all members of the dimension.|  
        |Hierarchy|Select a single hierarchy. The action will be associated with the hierarchy object only. Attribute hierarchies appear in the list only if their **AttributeHierarchyEnabled** and **AttributeHierarchyVisible** properties are set to **True**.|  
        |Hierarchy members|Select a single hierarchy. The action will be associated with all members of the selected hierarchy. Attribute hierarchies appear in the list only if their **AttributeHierarchyEnabled** and **AttributeHierarchyVisible** properties are set to **True**.|  
        |Level|Select a single level. The action will be associated with the level object only.|  
        |Level members|Select a single level. The action will be associated with all members of the selected level.|  
  
    -   In **Target object**, click the arrow at the right of the text box, and in the tree view that opens, click the object to which you want to attach the action, and then click **OK**.  
  
    -   (Optional.) In **Condition**, create an MDX expression to limit the target of the action. You can either type the expression manually, or you can drag items from the **Metadata** and **Functions** tabs.  
  
    -   From the **Type** drop-down list, select the type of action that you want to create. The following table lists the types of actions that are available.  
  
        |Type|Description|  
        |----------|-----------------|  
        |Dataset|Retrieves a dataset.|  
        |Proprietary|Performs an operation using an interface other than those listed in this table.|  
        |Rowset|Retrieves a rowset.|  
        |Statement|Runs an OLE DB command.|  
        |URL|Displays a Web page in an Internet browser.|  
  
    -   In **Action expression**, create an expression that defines the action. The expression must evaluate to a string. You can either type the expression manually, or you can drag items from the **Metadata** and **Functions** tabs.  
  
3.  (Optional.) Expand **Additional Properties**, and then perform one of the following steps:  
  
    -   From the **Invocation** drop-down list, specify how the action is invoked. The following table describes the available options for invoking an action.  
  
        |Option|Description|  
        |------------|-----------------|  
        |Interactive|The action is triggered by user interaction.|  
        |Batch|The action runs as a batch operation.|  
        |On open|The action runs when a user opens the cube.|  
  
    -   In **Application**, type the name of the application that is associated with the action. For example, if you create an action that takes a user to a particular Web site, the application associated with the action should be Microsoft Internet Explorer or another Web browser.  
  
        > [!NOTE]  
        >  Proprietary actions are not returned to the server unless the client application explicitly restricts the schema rowset to return only actions that match the name specified in **Application**.  
  
    -   In **Action Content**, if you are using the URL type, enclose the Internet address in quotes, For example, "http://www.adventure-works.com".  
  
    -   In **Description**, type a description for the action.  
  
    -   In **Caption**, type a caption or an MDX expression that evaluates to a caption. This caption is displayed to end users when the action is initiated. If you do not specify a caption, the name of the action is used instead.  
  
    -   From the **Caption is MDX** drop-down list, specify whether the caption is MDX. This field indicates to the server whether to evaluate the contents of **Caption** as an MDX expression.  
  
  
