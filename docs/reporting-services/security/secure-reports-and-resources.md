---
description: "Secure Reports and Resources"
title: "Secure Reports and Resources | Microsoft Docs"
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: security


ms.topic: conceptual
helpviewer_keywords: 
  - "security [Reporting Services], reports"
  - "security [Reporting Services], resources"
  - "reports [Reporting Services], security"
  - "confidential reports [Reporting Services]"
  - "resources [Reporting Services], security"
ms.assetid: 63cd55c7-fd2a-49e3-a3f8-59eb1a1c6e83
author: maggiesMSFT
ms.author: maggies
---
# Secure Reports and Resources
  You can set security for individual reports and resources to control the degree of access that users have to these items. By default, only users who are members of the **Administrators** built-in group can run reports, view resources, modify properties, and delete the items. All other users must have role assignments created for them that allow access to a report or resource.  
  
## Role-based Access to Reports and Resources  
 To grant access to reports and resources, you can allow users to inherit existing role assignments from a parent folder or create a new role assignment on the item itself.  
  
 In most cases, you will probably want to use the permissions that are inherited from a parent folder. Setting security on individual reports and resources should only be necessary if you want to hide the report or resource from users who do not need to know that the report or resource exists, or to increase the level of access for a report or item. These objectives are not mutually exclusive. You can restrict access to a report to a smaller set of users, and provide all or some of them with additional privileges to manage the report.  
  
 You may need to create multiple role assignments to achieve your objectives. For example, suppose you have a report that you want to make accessible to two users, Ann and Fernando, and to the Human Resource Managers group. Ann and Fernando must be able to manage the report, but the Human Resource Managers members need only to run it. To accommodate all of these users, you would create three separate role assignments: one to make Ann a content manager of the report, one to make Fernando a content manager of the report, and one to support view-only tasks for the Human Resource Managers group.  
  
 Once you set security on a report or resource, those settings stay with the item even if you move the item to a new location. For example, if you move a report that only a few people are authorized to access, the report continues to be available to just those users even if you move it to a folder that has a relatively open security policy.  
  
## Mitigating HTML Injection Attacks in a Published Report or Document  
 In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], reports and resources are processed under the security identity of the user who is running the report. If the report contains expressions, script, custom report items, or custom assemblies, the code runs under the user's credentials. If a resource is an HTML document that contains script, the script will run when the user opens the document on the report server. The ability to run script or code within a report is a powerful feature that comes with a certain level of risk. If the code is malicious, the report server and the user who is running the report are vulnerable to attack.  
  
 When granting access to reports and to resources that are processed as HTML, it is important to remember that reports are processed in full trust and that potentially malicious script might be sent to the client. Depending on browser settings, the client will execute the HTML at the level of trust that is specified in the browser.  
  
 You can mitigate the risk of running malicious script by taking the following precautions:  
  
-   Be selective when deciding who can publish content to a report server. Because the potential for publishing malicious content exists, you should limit users who can publish content to a small number of trusted users.  
  
-   All publishers should avoid publishing reports and resources that come from unknown or untrusted sources. If necessary, open the file in a text editor and look for suspicious script and URLs.  
  
## Report Parameters and Script Injection  
 Report Parameters provide flexibility for the overall report design and execution. However, this same flexibility can, in some cases be used by an attacker in luring attacks. To mitigate the risk of inadvertently running malicious scripts, only open rendered reports from trusted sources. It is recommended you consider the following scenario that is a potential HTML Renderer script injection attack:  
  
1.  A report contains a text box with the hyperlink action set to the value of a parameter which could contain malicious text.  
  
2.  The report is published to a report server or otherwise made available in such a way that the report parameter value can be controlled from the URL of a web page.  
  
3.  An attacker creates a link to the web page or report server specifying the value of the parameter in the form "javascript:\<malicious script here>" and sends that link to someone else in a luring attack.  
  
## Mitigating Script Injection Attacks in a Hyperlink in a Published Report or Document  
 Reports can contain embedded hyperlinks in the value of the Action property on a report item or part of a report item. Hyperlinks can be bound to data that is retrieved from an external data source when the report is processed. If a malicious user modifies the underlying data, the hyperlink might be at risk for scripting exploits. If a user clicks the link in the published or exported report, malicious script could run.  
  
 To mitigate the risk of including links in a report that inadvertently run malicious scripts, only bind hyperlinks to data from trusted sources. Verify that data from the query results and the expressions that bind data to hyperlinks do not create links that can be exploited. For example, do not base a hyperlink on an expression that concatenates data from multiple dataset fields. If necessary, browse to the report and use "View Source" to check for suspicious scripts and URLs.  
  
## Mitigating SQL Injection Attacks in a Parameterized Report  
 In any report that includes a parameter of type **String**, be sure to use an available values list (also known as a valid values list) and ensure that any user running the report has only the permissions required to view the data in the report. When you define a parameter of type **String**, the user is presented with a text box that can take any value. An available values list limits the values that can be entered. If the report parameter is tied to a query parameter and you do not use an available values list, it is possible for a report user to type SQL syntax into the text box, potentially opening the report and your server to a SQL injection attack. If the user has sufficient permissions to execute the new SQL statement, it may produce unwanted results on the server.  
  
 If a report parameter is not tied to a query parameter and the parameter values are included in the report, it is possible for a report user to type expression syntax or a URL into the parameter value and render the report to Excel or HTML. If another user then views the report and clicks the rendered parameter contents, the user may inadvertently execute the malicious script or link.  
  
 To mitigate the risk of inadvertently running malicious scripts, open rendered reports only from trusted sources.  
  
> [!NOTE]  
>  In previous releases of the documentation, an example of creating a dynamic query as an expression was included. This type of query creates a vulnerability to SQL injection attacks and therefore is not recommended.  
  
## Securing Confidential Reports  
 Reports that contain confidential information should be secured at the data-access level, by requiring users to provide credentials to access sensitive data. For more information, see [Specify Credential and Connection Information for Report Data Sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md). You can also secure a folder to make it inaccessible to unauthorized users. For more information, see [Secure Folders](../../reporting-services/security/secure-folders.md).  
  
## See Also  
 [Create and Manage Role Assignments](../../reporting-services/security/create-and-manage-role-assignments.md)   
 [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)   
 [Secure Shared Data Source Items](../../reporting-services/security/secure-shared-data-source-items.md)   
 [Store Credentials in a Reporting Services Data Source](../../reporting-services/report-data/store-credentials-in-a-reporting-services-data-source.md)  
  
  
