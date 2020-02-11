---
title: "Options (Environment: Fonts and Colors Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
ms.assetid: ea3aa222-538d-485f-99dc-01eb02cdcfea
author: stevestein
ms.author: sstein
manager: craigg
---
# Options (Environment: Fonts and Colors Page)
  The **Options** dialog box lets you establish a custom font and color scheme for various user interface elements in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. On the **Tools** menu, click **Options,** expand the **Environment** folder, and select **Fonts and Colors**.  
  
 Color scheme changes do not take effect during the session in which you make them. You can evaluate color changes by opening another instance of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and producing the conditions under which you expect your changes to apply.  
  
## UIElement List  
 **Show settings for**  
 Lists all the user interface elements for which you can change font and color schemes. After selecting an item from this list you can customize color settings for the item selected in **Display items**.  
  
|Term|Definition|  
|----------|----------------|  
|Text Editor|Changes to font style, size, and color display settings for Text Editor affect the appearance of text in your default text editor. Documents opened in a text editor outside [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] will not be affected by these settings.|  
|Printer|Changes to font style, size, and color display settings for Printer affect the appearance of text in printed documents.<br /><br /> Tip: As needed, you can select a different default font for printing than that used for display in the Text Editor. This can be useful when printing code that contains both single-byte and double-byte characters.|  
|[All Text Tool Windows **]**|Changes to font style, size, and color display settings for this item affect the appearance of text in tool windows that have output panes in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For example, Output window, TextResults window, and so on.<br /><br /> Note: Changes to the text of [All Text Tool Windows] items do not take effect during the session in which you make them. You can evaluate such changes by opening another instance of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].|  
|Find Results Window|Changes to font style, size, and color display settings for this item affect the appearance of text in the FindResults window.|  
|Output Window|Changes to font style, size, and color display settings for this item affect the appearance of text in the Output window.|  
|Grid Results|Changes to the font style, size, and color display settings for this item affect the appearance of text in the **Grid Results** area of the Query window.|  
|Execution Plan|Changes to font style, size, and color display settings for this item affect the appearance of text in the Execution Plan of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssEW](../../includes/ssew-md.md)] queries.|  
|Text Results|Changes to font style, size, and color display settings for this item affect the appearance of text in the **Text Results** area of the Query window.|  
|Business Intelligence Designers|Changes to font style, size, and color display settings for this item affect the appearance of text in the BusinessIntelligenceDesignerswindow.|  
  
 **Use Defaults**  
 The **Use Defaults** button resets the value for the default font and color values of the list item that you selected from the **Show settings for** list.  
  
 **Font (bold type indicates fixed-width fonts)**  
 Lists all the fonts installed on your system. When this drop-down list is first opened, the current font for the element you chose from the **Show settings for** list is selected. Fixed fonts - which are easier to align in an editor - appear in bold.  
  
 **Size**  
 Lists available point sizes for the selected font. Changing the size of the font affects all **Display items** entries for a **Show settings for** selection.  
  
 **Display items**  
 Lists the items for which you can modify the foreground and background color.  
  
> [!NOTE]  
>  The default display item is Text. As such, properties assigned to Text will be overridden by properties assigned to other display items. For example, if you assign the color blue to **Text** and the color green to Identifier, all identifiers will appear in green. In this example, Identifier properties override Text properties.  
  
 Some display items include:  
  
-   Indicator Margin: The margin at the left of the Code Editor where breakpoints and bookmark icons are displayed.  
  
-   Collapsible Text: A block of text or code that can be switched into and out of view within the CodeEditor (XML only).  
  
 **Item foreground**  
 Lists the available colors you can choose for the foreground of the item selected in **Display items**. Because some items are related, a consistent display scheme should be maintained; for example, changing the foreground color of text also changes the foreground color for elements such as String.  
  
 **Custom**  
 Displays the **Color** dialog box, where you set a custom color for the item selected in the **Display items** list.  
  
> [!NOTE]  
>  Your ability to define custom colors may be limited by the color settings for your computer display. For example, if your computer is set to display 256 colors and you select a custom color from the **Color** dialog box, [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] chooses to the closest available value in **Basic colors** as the default, and displays the color black in the **Color** dialog box.  
  
 **Item background**  
 Provides a color palette from which you can choose a background color for the item selected in **Display items**. Because some items are related, a consistent display scheme should be maintained; for example, changing the background color of text also changes the background color for elements such as SQL String.  
  
 **Custom**  
 Displays the **Color** dialog box, where you set a custom color for the item selected in the **Display items** list.  
  
 **Bold**  
 Select this check box to display the text of selected displayitems in bold font. Bold text is easier to identify in an editor.  
  
 **Sample**  
 Displays a sample of the font style, size, and color scheme for the selected values in **Show settings for** and **Display items**. You can use this text box to preview the results as you experiment with different formatting options.  
  
## See Also  
 [Color Coding in Query Editors](../../relational-databases/scripting/color-coding-in-query-editors.md)   
 [Options &#40;Text Editor: Editor Tab and Status Bar Page&#41;](../../database-engine/options-text-editor-editor-tab-and-status-bar-page.md)  
  
  
