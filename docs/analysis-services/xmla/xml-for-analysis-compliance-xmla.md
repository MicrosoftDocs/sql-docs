---
title: "XML for Analysis (XMLA) Compliance | Microsoft Docs"
ms.date: 05/30/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# XML for Analysis (XMLA) Compliance
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]  

  The XML for Analysis 1.1 specification describes an open standard that supports data access to data sources that reside on the World Wide Web. This topic details the level of compliance with the XML for Analysis 1.1 specification that is supported by Azure Analysis Services and SQL Server Analysis Services.  
  
## Compliant items  
Analysis Services complies with all mandatory items listed in the XML for Analysis 1.1 specification. In addition, Analysis Services implements the following optional item described in the XML for Analysis 1.1 specification.  
  
|Item|Specification|Implementation|  
|----------|-------------------|--------------------|  
|Session support|The SOAP header elements listed in "Support for Statefulness in XML for Analysis" section of the XML for Analysis 1.1 specification.|All listed SOAP header elements are supported by Analysis Services, as described within the specification.|  
  
## Extensions  
 Analysis Services also extends the XML for Analysis 1.1 specification to support the following additional features and capabilities.  
  
|Item|Specification|Implementation|  
|----------|-------------------|--------------------|  
|Protocol negotiation|No information contained in the XML for Analysis 1.1 specification|ProtocolCapabilities header element added by Analysis Services to support negotiation of protocol capabilities.|  
|XML for Analysis (XMLA) commands supported by the Discover method|The following commands are supported by the XML for Analysis 1.1 specification:<br /><br /> [Statement Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/statement-element-xmla.md)|The following commands are supported by Analysis Services:<br /><br /> [Alter Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/alter-element-xmla.md)<br /><br /> [Backup Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/backup-element-xmla.md)<br /><br /> [Batch Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md)<br /><br /> [BeginTransaction Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/begintransaction-element-xmla.md)<br /><br /> [Cancel Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/cancel-element-xmla.md)<br /><br /> [ClearCache Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/clearcache-element-xmla.md)<br /><br /> [CommitTransaction Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/committransaction-element-xmla.md)<br /><br /> [Create Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/create-element-xmla.md)<br /><br /> [Delete Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/delete-element-xmla.md)<br /><br /> [DesignAggregations Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/designaggregations-element-xmla.md)<br /><br /> [Drop Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/drop-element-xmla.md)<br /><br /> [Insert Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/insert-element-xmla.md)<br /><br /> [Lock Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/lock-element-xmla.md)<br /><br /> [MergePartitions Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/mergepartitions-element-xmla.md)<br /><br /> [NotifyTableChange Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/notifytablechange-element-xmla.md)<br /><br /> [Process Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md)<br /><br /> [Restore Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md)<br /><br /> [RollbackTransaction Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/rollbacktransaction-element-xmla.md)<br /><br /> [Statement Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/statement-element-xmla.md)<br /><br /> [Subscribe Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/subscribe-element-xmla.md)<br /><br /> [Synchronize Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md)<br /><br /> [Unlock Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/unlock-element-xmla.md)<br /><br /> [Update Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/update-element-xmla.md)<br /><br /> [UpdateCells Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/updatecells-element-xmla.md)|  
|Column errors in tabular rowsets|Not listed in XML for Analysis 1.1 specification.|The [Error](../../analysis-services/xmla/xml-elements-properties/error-element-xmla.md) element is used by Analysis Services to report errors for column elements contained in a [row](../../analysis-services/xmla/xml-elements-properties/error-element-xmla.md) element.|  
  
## See also
 [XML for Analysis  &#40;XMLA&#41; Reference](../../analysis-services/xmla/xml-for-analysis-xmla-reference.md)  
  
  
