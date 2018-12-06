---
title: "Add a Hyperlink to a URL (Report Builder and SSRS) | Microsoft Docs"
ms.date: 09/07/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: d3392c0b-7b62-4d27-bc04-2bd0c5487d08
author: maggiesMSFT
ms.author: maggies
---
# Add a Hyperlink to a URL (Report Builder and SSRS)
Learn how to add hyperlink actions to text boxes, images, charts, and gauges in [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)]  paginated reports. Links can go to other reports, to bookmarks in a report, or to  static or dynamic URLs. 

 You can add a hyperlink action to any item that has an **Action** property, for example, a text box, an image, or a calculated series in a chart. When the user clicks that report item, the action that you define takes place.  
  
*   You can **add a hyperlink that will open a browser with a URL** that you specify. The hyperlink can be a static URL or an expression that evaluates to a URL. If you have a field in a database that contains URLs, the expression can contain that field, resulting in a dynamic list of hyperlinks in the report. Make sure your report readers have access to the URL that you provide.  
   
*  You can also **specify URLs to create drillthroughs** to reports on any report server that you and your users have permission to view using URL requests to the report server. For example, you can specify a report and hide the document map for the user when they first view the report. For more information, see [URL Access &#40;SSRS&#41;](../../reporting-services/url-access-ssrs.md) and [Specifying Paths to External Items &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/specifying-paths-to-external-items-report-builder-and-ssrs.md).
 
 *  And you can **add a bookmark to a specific place** in the same report. 
  
Try adding hyperlinks with sample data in [Tutorial: Format Text &#40;Report Builder&#41;](../../reporting-services/tutorial-format-text-report-builder.md).  
  
> [!NOTE]  
>  Links that are bound to dataset fields can be vulnerable to tampering for malicious purposes. For more information, see [Secure Reports and Resources](../../reporting-services/security/secure-reports-and-resources.md).  
  
## To add a hyperlink and...   
  
1.  In report design view, right-click the text box, image, or chart to which you want to add a link and then click **Properties**.  
  
2.  In the Properties dialog box, click the **Action** tab. Read on for information about your options.  

## ... add drillthrough to another report

1. On the **Action** tab, select **Go to report**. 

2. Specify the target report and parameters you want to use. The parameter names must match the parameters defined for the target report. 

3. Use the **Add** and **Delete** buttons to add and remove parameters, and the up and down arrows to order the list of parameters.

4.  Type or select a **Value** to pass for the named parameter in the drillthrough report. Click the Expression (fx) button to edit the expression.

5. Select **Omit** to prevent the parameter from running. By default, this check box is cleared and not active. To select the check box, click the Expression (fx) button and either type True or create an expression. The check box is selected when you click **OK** in the Expression dialog box.
  
   See [Add a Drillthrough Action on a Report](../../reporting-services/report-design/add-a-drillthrough-action-on-a-report-report-builder-and-ssrs.md) for more information. 
   
6. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
   
## ... add a bookmark

You can link to bookmarks to a location in the current report. To link to a bookmark, you must first set the **Bookmark** property of a report item. To set the **Bookmark** property, select a report item, and in the Properties pane, type a value or expression for the bookmark ID; for example, SalesChart or 5TopSales.

1. On the **Action** tab, select **Go to bookmark**. 

2. Type or select the bookmark ID for the report to jump to. Click the Expression (fx) button to change the expression. 

   The bookmark ID can be either a static ID or an expression that evaluates to a bookmark ID. The expression can include a field that contains a bookmark ID.
   
   See [Add a Bookmark to a Report](../../reporting-services/report-design/add-a-bookmark-to-a-report-report-builder-and-ssrs.md) for more information.
   
3. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  

## ... add a hyperlink 
  
1. On the **Action** tab, select **Go to URL**. An additional section appears in the dialog box for this option.  
  
4.  In **Select URL**, type or select a URL or an expression that evaluates to a URL, or click the drop-down arrow and click the name of a field that contains a URL. 

    For an item published to a report server configured for native mode, use a full or relative path. For example, `https://<servername>/images/image1.jpg`. 
    
    For an item published to a report server configured in SharePoint integrated mode, use a fully qualified URL. For example, `https://<SharePointservername>/<site>/Documents/images/image1.jpg`.
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  

## After you add a hyperlink
  
1.  (Optional) The text is not automatically formatted as a link. For text, it is helpful to change the color and effect of the text to indicate that the text is a link. For example, change the color to blue and the effect to underline in the **Font** section in the Home tab of the Ribbon.  
  
7.  To test the link, click **Run** to preview the report, and then click the report item that you set this link on.  
  
## See Also  
 [Interactive Sort, Document Maps, and Links &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/interactive-sort-document-maps-and-links-report-builder-and-ssrs.md)   
 [Create a Document Map &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/create-a-document-map-report-builder-and-ssrs.md)  
  
  
