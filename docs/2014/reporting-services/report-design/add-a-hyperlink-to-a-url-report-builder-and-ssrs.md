---
title: "Add a Hyperlink to a URL (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: d3392c0b-7b62-4d27-bc04-2bd0c5487d08
author: markingmyname
ms.author: maghan
manager: kfile
---
# Add a Hyperlink to a URL (Report Builder and SSRS)
  You can add a hyperlink to a report item when you want your users to be able to click a link in a report and open a browser to the URL that you specify. A hyperlink can be a static URL or an expression that evaluates to a URL. If you have a field in a database that contains URLs, the expression can contain that field, resulting in a dynamic list of hyperlinks in the report. You can add hyperlinks to text boxes, images, charts, and gauges. You must ensure that the user has access to the URL that you provide.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
 You can also specify URLs to reports on any report server that you and your users have permission to view using URL requests to the report server. For example, you can specify a report and hide the document map for the user when they first view the report. For more information, see [URL Access &#40;SSRS&#41;](../url-access-ssrs.md) in the [Reporting Services documentation](https://go.microsoft.com/fwlink/?linkid=121312) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
 You can add a hyperlink to a URL to any item that has an **Action** property, for example, a text box, an image, or a calculated series in a chart. When the user clicks that report item, the action that you define takes place. For more information, see the [Action Properties Dialog Box &#40;Report Builder and SSRS&#41;](../action-properties-dialog-box-report-builder-and-ssrs.md) and [Specifying Paths to External Items &#40;Report Builder and SSRS&#41;](specifying-paths-to-external-items-report-builder-and-ssrs.md).  
  
 To quickly get started, see [Tutorial: Format Text &#40;Report Builder&#41;](../tutorial-format-text-report-builder.md).  
  
> [!NOTE]  
>  Links that are bound to dataset fields can be vulnerable to tampering for malicious purposes. For more information, see [Secure Reports and Resources](../security/secure-reports-and-resources.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][Books Online](https://go.microsoft.com/fwlink/?LinkId=154888) on msdn.microsoft.com.  
  
### To add a hyperlink  
  
1.  In report design view, right-click the text box, image, or chart to which you want to add a link and then click **Properties**.  
  
2.  In the Properties dialog box, click **Action**.  
  
3.  Select **Go to URL**. An additional section appears in the dialog box for this option.  
  
4.  In **Select URL**, type or select a URL or an expression that evaluates to a URL, or click the drop-down arrow and click the name of a field that contains a URL.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
6.  (Optional) The text is not automatically formatted as a link. For text, it is helpful to change the color and effect of the text to indicate that the text is a link. For example, change the color to blue and the effect to underline in the **Font** section in the Home tab of the Ribbon.  
  
7.  To test the link, click **Run** to preview the report, and then click the report item that you set this link on.  
  
## See Also  
 [Interactive Sort, Document Maps, and Links &#40;Report Builder and SSRS&#41;](interactive-sort-document-maps-and-links-report-builder-and-ssrs.md)   
 [Create a Document Map &#40;Report Builder and SSRS&#41;](create-a-document-map-report-builder-and-ssrs.md)  
  
  
