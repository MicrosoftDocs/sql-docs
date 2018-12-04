---
title: "Domain Management: Domain List | Microsoft Docs"
ms.custom: ""
ms.date: "11/08/2011"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dqs.dm.domainlist.f1"
ms.assetid: 8df305f0-97ea-4226-811b-979ed862e1f0
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Domain Management: Domain List

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  This topic describes the controls in the Domains list of the **Domain Management** page in [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS). Use this pane to select a domain to perform management operations on. The same pane is used for all tabbed pages in the **Domain Management** page.  
  
## Options  
  
### Domains List  
 **Domain**  
 This list shows all domains in the knowledge base. Operations that you perform in the tabbed pages in the right-hand pane will be performed on the domain that is selected in the list. For more information, see  
  
 **Create a Composite Domain**  
 Create a new composite domain in the knowledge base. This command will display the **Create a Composite Domain** dialog box. This command is available either by right-clicking a domain or by clicking the icon above the domain list. For more information, see [Create a Composite Domain](../data-quality-services/create-a-composite-domain.md).  
  
 **Create a Domain**  
 Create a new domain in the knowledge base. This command will display the **Create Domain** dialog box. This command is available either by right-clicking a domain or by clicking the icon above the domain list. For more information, see [Create a Domain](../data-quality-services/create-a-domain.md).  
  
 **Create a copy of the selected domain**  
 Create an exact copy of the selected domain, and add it to the knowledge base. Its name will be the name of the domain that it was created from, plus " - Copy" appended to the name. This command is available either by right-clicking a domain and then clicking **Create a copy**, or by clicking the icon above the domain list. It is not available for a composite domain.  
  
 **Import Domain from Data File**  
 Import a domain from a .dqs file. This command displays the **Import from Data File** dialog box that enables you to browse the file system and select a .dqs file for a single domain or a composite domain. This command is available by clicking the icon above the domain list. For more information, see [Import a Domain from a .dqs File](../data-quality-services/import-a-domain-from-a-dqs-file.md).  
  
 **Delete Domain**  
 Delete the selected domain from the knowledge base. This command displays the **SQL Server Data Quality Services** dialog box. If you click **Yes**, the domain and all its data will be permanently deleted. This command is available either by right-clicking a domain or by clicking the icon above the domain list.  
  
 **Create a Linked Domain**  
 Create a domain that is linked to the selected domain. This command displays the **Create domain** dialog box. This command is available by right-clicking a domain, and then clicking **Create a Linked Domain** that is linked to the selected domain. The domain that you are linking to is shown in the Create Domain dialog box. The command is not available for a composite domain. There is no command available to unlink two domains; to do so, delete the linked domain. A linked domain cannot be created to a linked domain. For more information, see [Create a Linked Domain](../data-quality-services/create-a-linked-domain.md).  
  
 A linked domain has the same values as the domain that it is linked to. Only the name and properties of the domain are different. If you change a domain rule, domain value, reference data link, or term-based relation in the domain that is linked to, the domain rule, domain value, reference data link, or term-based relation in the linked domain will also change. Also, if you change a value in the linked domain, the change will also be made in the domain linked to.  
  
 **Export Knowledge Base**  
 Export the entire knowledge base to a .dqs file. This command displays the **Export to Data File** dialog box. This command is available by clicking the **Export Knowledge Base data** icon at the top of the page, or under **Export** in the context menu of the domains in the domain list pane. For more information, see [Export a Knowledge Base to a .dqs File](../data-quality-services/export-a-knowledge-base-to-a-dqs-file.md).  
  
 **Export Domain**  
 Export the domain to a .dqs file. This command displays the **Export to Data File** dialog box. This command is available in the **Export** menu in the menu bar at the top of the page, or by right-clicking in the domain list pane. For more information, see [Export a Domain to a .dqs File](../data-quality-services/export-a-domain-to-a-dqs-file.md).  
  
  
