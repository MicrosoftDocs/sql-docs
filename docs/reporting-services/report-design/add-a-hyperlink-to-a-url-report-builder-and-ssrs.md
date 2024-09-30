---
title: "Add a hyperlink to a URL in a paginated report"
description: Discover how to add hyperlink actions to text boxes, images, charts, and gauges in paginated reports in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Add a hyperlink to a URL in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

Learn how to add hyperlink actions to text boxes, images, charts, and gauges in paginated reports. Links can go to other reports, to bookmarks in a report, or to  static or dynamic URLs.

 You can add a hyperlink action to any item that has an **Action** property, for example, a text box, an image, or a calculated series in a chart. When the user selects that report item, the action that you define takes place.  
  
* You can **add a hyperlink that opens a browser with a URL** that you specify. The hyperlink can be a static URL or an expression that evaluates to a URL. If you have a field in a database that contains URLs, the expression can contain that field, resulting in a dynamic list of hyperlinks in the report. Make sure your report readers have access to the URL that you provide.  
   
* You can also **specify URLs to create drillthroughs** to reports on any report server that you and your users have permission to view using URL requests to the report server. For example, you can specify a report and hide the document map for the user when they first view the report. For more information, see [URL access](../../reporting-services/url-access-ssrs.md) and [Specify paths to external items &#40;Report Builder&#41;](../../reporting-services/report-design/specifying-paths-to-external-items-report-builder-and-ssrs.md).
 
 * And you can **add a bookmark to a specific place** in the same report. 
  
Try adding hyperlinks with sample data in [Tutorial: Format text &#40;Report Builder&#41;](../../reporting-services/tutorial-format-text-report-builder.md).  
  
> [!NOTE]  
>  Links that are bound to dataset fields can be vulnerable to tampering for malicious purposes. For more information, see [Secure reports and resources](../../reporting-services/security/secure-reports-and-resources.md).  
  
## Add a hyperlink and ...   
  
1.  In report design view, right-click the text box, image, or chart to which you want to add a link and then select **Properties**.  
  
1.  In the **Properties** dialog, select the **Action** tab. Read on for information about your options.  

## ... add drillthrough to another report

1. On the **Action** tab, select **Go to report**. 

1. Specify the target report and parameters you want to use. The parameter names must match the parameters defined for the target report. 

1. Use the **Add** and **Delete** buttons to add and remove parameters, and the up and down arrows to order the list of parameters.

1.  Enter or select a **Value** to pass for the named parameter in the drillthrough report. Select the Expression (fx) button and edit the expression.

1. Select **Omit** to prevent the parameter from running. By default, this box is cleared and not active. To choose the box, select the Expression (fx) button and either type True or create an expression. The box is selected when you select **OK** in the Expression dialog.
  
   For more information, see [Add a drillthrough action on a report](../../reporting-services/report-design/add-a-drillthrough-action-on-a-report-report-builder-and-ssrs.md). 
   
1. Select **OK**.
   
## ... add a bookmark

You can link to bookmarks to a location in the current report. To link to a bookmark, you must first set the **Bookmark** property of a report item. To set the **Bookmark** property, select a report item, and in the **Properties** pane, enter a value or expression for the bookmark ID. For example, enter `SalesChart` or `5TopSales`.

1. On the **Action** tab, select **Go to bookmark**. 

1. Enter or select the bookmark ID for the report to jump to. Select the Expression (fx) button and change the expression. 

   The bookmark ID can be either a static ID or an expression that evaluates to a bookmark ID. The expression can include a field that contains a bookmark ID.
   
   For more information, see [Add a bookmark to a report](../../reporting-services/report-design/add-a-bookmark-to-a-report-report-builder-and-ssrs.md).
   
1. Select **OK**.

## ... add a hyperlink 
  
1. On the **Action** tab, select **Go to URL**. Another section appears in the dialog for this option.  
  
1.  In **Select URL**, enter or select a URL or an expression that evaluates to a URL, or select the list arrow and choose the name of a field that contains a URL. 

    For an item published to a report server configured for native mode, use a full or relative path. For example, use `https://<servername>/images/image1.jpg`. 
    
    For an item published to a report server configured in SharePoint integrated mode, use a fully qualified URL. For example, use `https://<SharePointservername>/<site>/Documents/images/image1.jpg`.
  
1.  Select **OK**.

## After you add a hyperlink
  
1.  (Optional) The text isn't automatically formatted as a link. For text, you might find it helpful to change the color and effect of the text to indicate that the text is a link. For example, change the color to blue and the effect to underline in the **Font** section in the **Home** tab of the Ribbon.  
  
1.  To test the link, select **Run** to preview the report, and then choose the report item that you set this link on.  
  
## Related content

- [Interactive sort, document maps, and links &#40;Report Builder&#41;](../../reporting-services/report-design/interactive-sort-document-maps-and-links-report-builder-and-ssrs.md)
- [Create a document map &#40;Report Builder&#41;](../../reporting-services/report-design/create-a-document-map-report-builder-and-ssrs.md)
