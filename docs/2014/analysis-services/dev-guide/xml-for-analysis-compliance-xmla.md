---
title: "XML for Analysis Compliance (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "compliance [XML for Analysis]"
  - "XML for Analysis, compliance"
  - "XMLA, extended functionality"
  - "XML for Analysis, extended functionality"
  - "XMLA, compliance"
  - "extending XML for Analysis"
ms.assetid: d987d320-5581-4454-ad45-68e3a22175b6
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# XML for Analysis Compliance (XMLA)
  The XML for Analysis 1.1 specification describes an open standard that supports data access to data sources that reside on the World Wide Web. This topic details the level of compliance with the XML for Analysis 1.1 specification that is supported by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
## Compliant Items  
 [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] complies with all mandatory items listed in the XML for Analysis 1.1 specification. In addition, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] implements the following optional item described in the XML for Analysis 1.1 specification.  
  
|Item|Specification|Implementation|  
|----------|-------------------|--------------------|  
|Session support|The SOAP header elements listed in "Support for Statefulness in XML for Analysis" section of the XML for Analysis 1.1 specification.|All listed SOAP header elements are supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], as described within the specification.|  
  
## Extensions  
 [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] also extends the XML for Analysis 1.1 specification to support the following additional features and capabilities.  
  
|Item|Specification|Implementation|  
|----------|-------------------|--------------------|  
|Protocol negotiation|No information contained in the XML for Analysis 1.1 specification|[ProtocolCapabilities](../../../2014/analysis-services/dev-guide/protocolcapabilities-element-xmla.md) header element added by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] to support negotiation of protocol capabilities.|  
|XML for Analysis (XMLA) commands supported by the Discover method|The following commands are supported by the XML for Analysis 1.1 specification:<br /><br /> -   [Statement Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/statement-element-xmla.md)|The following commands are supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]:<br /><br /> -   [Alter Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/alter-element-xmla.md)<br />-   [Backup Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/backup-element-xmla.md)<br />-   [Batch Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/batch-element-xmla.md)<br />-   [BeginTransaction Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/begintransaction-element-xmla.md)<br />-   [Cancel Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/cancel-element-xmla.md)<br />-   [ClearCache Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/clearcache-element-xmla.md)<br />-   [CommitTransaction Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/committransaction-element-xmla.md)<br />-   [Create Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/create-element-xmla.md)<br />-   [Delete Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/delete-element-xmla.md)<br />-   [DesignAggregations Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/designaggregations-element-xmla.md)<br />-   [Drop Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/drop-element-xmla.md)<br />-   [Insert Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/insert-element-xmla.md)<br />-   [Lock Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/lock-element-xmla.md)<br />-   [MergePartitions Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/mergepartitions-element-xmla.md)<br />-   [NotifyTableChange Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/notifytablechange-element-xmla.md)<br />-   [Process Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/process-element-xmla.md)<br />-   [Restore Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/restore-element-xmla.md)<br />-   [RollbackTransaction Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/rollbacktransaction-element-xmla.md)<br />-   SetPasswordEncryptionKey Element<br />-   [Statement Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/statement-element-xmla.md)<br />-   [Subscribe Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/subscribe-element-xmla.md)<br />-   [Synchronize Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/synchronize-element-xmla.md)<br />-   [Unlock Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/unlock-element-xmla.md)<br />-   [Update Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/update-element-xmla.md)<br />-   [UpdateCells Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/updatecells-element-xmla.md)|  
|Column errors in tabular rowsets|Not listed in XML for Analysis 1.1 specification.|The [Error](../../../2014/analysis-services/dev-guide/error-element-xmla.md) element is used by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] to report errors for column elements contained in a [row](../../../2014/analysis-services/dev-guide/error-element-xmla.md) element.|  
  
## See Also  
 [XML for Analysis  &#40;XMLA&#41; Reference](../../../2014/analysis-services/dev-guide/xml-for-analysis-xmla-reference.md)  
  
  