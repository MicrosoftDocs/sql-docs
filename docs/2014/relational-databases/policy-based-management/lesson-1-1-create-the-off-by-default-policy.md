---
title: "Create the Off By Default Policy | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 98fde3c5-297c-4d95-981e-95700bbf5ccd
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Create the Off By Default Policy
  This task creates a condition named Mail Off that is based on the Surface Area Configuration facet. Then, it creates a policy named Off By Default.  
  
### To create the Mail Off condition  
  
1.  In Object Explorer, expand **Management**, expand **Policy Management**, expand **Facets**, right-click **Surface Area Configuration**, and then click **New Condition**.  
  
2.  In the **Create New Condition** dialog box, in the **Name** box, type **Mail Off**.  
  
3.  In the **Facet** box, confirm that **Surface Area Configuration** facet is selected.  
  
4.  In the **Expression** area, in the **Field** box, select **@DatabaseMailEnabled**, in the **Operator** box select **=**, and in the **Value** select **False**.  
  
5.  On the **Description** page, type a description of the condition, and then click **OK** to create the condition.  
  
### To create the Off By Default policy  
  
1.  In Object Explorer, right-click **Surface Area Configuration**, and then click **New Policy**.  
  
2.  In the **Create New Policy** dialog box, in the **Name** box, type **Off By Default**.  
  
3.  Leave the **Enabled** checkbox unchecked. The **Enabled** checkbox applies to automated policies, and this policy will be executed on demand.  
  
4.  In the **Check condition** checkbox, scroll down to the **Surface Area Configuration** area, and then select **Mail Off** as the condition to check.  
  
5.  The **Against targets** box will be blank because this is a server-scoped policy.  
  
6.  In the **Evaluation Mode** checkbox, select **On demand** as the evaluation mode.  
  
7.  In the **Server restriction** checkbox, select **None**.  
  
8.  On the **Description** page, type a description of the policy.  
  
9. You can provide a hyperlink to a Web page for your policies in the **Additional help hyperlink** area. In the **Text to display** box, type the text that will appear for the hyperlink.  
  
10. In the **Address** box, type a hyperlink to a Help page, such as the home page for the IT department of your company.  
  
11. To confirm the address by opening the Web page, click **Test Link**.  
  
12. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## Next Task in Lesson  
 [Configure a Server to Run the Off By Default Policy](lesson-1-2-configure-a-server-to-run-the-off-by-default-policy.md)  
  
  
