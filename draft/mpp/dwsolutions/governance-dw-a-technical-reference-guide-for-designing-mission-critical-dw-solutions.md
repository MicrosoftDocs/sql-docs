---
title: "Governance (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6e9ec7b2-37bc-4bcc-b895-9654f8356172
caps.latest.revision: 2
manager: jeffreyg
---
# Governance (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
<?xml version="1.0" encoding="utf-8"?>
<developerConceptualDocument xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://ddue.schemas.microsoft.com/authoring/2003/5 http://clixdevr3.blob.core.windows.net/ddueschema/developer.xsd">
  <summary />
  <introduction>
    <table xmlns:caps="http://schemas.microsoft.com/build/caps/2013/11">
      <tbody>
        <tr>
          <TD>
            <para>
              <embeddedLabel>Want more guides like this one?</embeddedLabel> Go to <externalLink><linkText>Technical Reference Guides for Designing Mission-Critical Solutions</linkText><linkUri>http://technet.microsoft.com/en-us/sqlserver/hh273157</linkUri></externalLink>.</para>
          </TD>
        </tr>
      </tbody>
    </table>
    <para>While governance is not an easily pointed to set of IT features, understanding and deconstructing governance is important because the policies set by standards boards impact which features are required (for example, cell-based encryption, and data auditing). As an example, in the U.S.A, regulations such as the Health Insurance Portability and Accountability Act (HIPAA), the Health Level Seven International (HL7), Sarbanes-Oxley (SOX), and the payment card industry (PCI) impact a wide variety of organizations. The regulatory requirements and organizational policies can drive the decision to include or remove Microsoft SQL Server from tier-one enterprise consideration because enterprises must follow these standards as part of the cost of doing business.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide some general information about governance and compliance. Since regulatory organizations provide very few best practices for governance, Microsoft and enterprise customers rely on partner vendors to define how to implement systems that are in compliance. (Note that the full URLs for the hyperlinked text are provided in the Appendix at the end of this document.) </para>
      <list class="bullet">
        <listItem>
          <para>The website <externalLink><linkText>SQL Server 2008: Compliance</linkText><linkUri>http://www.microsoft.com/sqlserver/2008/en/us/compliance.aspx</linkUri></externalLink><superscript>1</superscript> is the main site for information about SQL Server compliance, including an overview of governance. Within this site, the following sections are of particular interest:</para>
          <list class="bullet">
            <listItem>
              <para>The Securing the platform section of the site provides guidance and references to secure the entire server environment end-to-end, rather than just securing the database.</para>
            </listItem>
            <listItem>
              <para>The Controlling identity and separation of duties section provides guidance and references about the basics of identity and access control in addition to the policies surrounding the separation of duties.</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>The white paper <externalLink><linkText>Reaching Compliance: SQL Server 2008 Compliance </linkText><linkUri>http://sqlcat.com/whitepapers/archive/2008/11/15/reaching-compliance-sql-server-2008-compliance-guide.aspx</linkUri></externalLink><externalLink><linkText>Guide</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2008/11/15/reaching-compliance-sql-server-2008-compliance-guide.aspx</linkUri></externalLink><superscript>2</superscript> includes a deep dive into understanding compliance and its impact through regulatory requirements and organization policies. </para>
        </listItem>
        <listItem>
          <para>The <externalLink><linkText>Security Standards Compliance</linkText><linkUri>http://msdn.microsoft.com/library/bb326717.aspx</linkUri></externalLink><superscript>3</superscript> section of the SQL Server 2008 R2 Books Online provides description and configuration procedures for common security criteria certifications.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <para>This section provides questions and issues to consider when working with your customers.</para>
      <list class="bullet">
        <listItem>
          <para>Microsoft does not have the deep domain and regulatory expertise in the various industries where its products are used. When working with customers understand their regulatory requirements and work with them and the partners to tailor solutions leveraging the security oriented features to meet the necessary regulatory governance. </para>
        </listItem>
        <listItem>
          <para>As you work with the customers and partners, consider how Microsoft as an organization work more closely with them in meeting their governance needs. Specifically how to better:</para>
          <list class="bullet">
            <listItem>
              <para>Receive input on necessary features and technologies, particularly in relation to local regulations.</para>
            </listItem>
            <listItem>
              <para>Provide guidance to the community on how to use Microsoft technologies for governance and compliance.</para>
            </listItem>
            <listItem>
              <para>Provide end-to-end governance solutions for customers.</para>
            </listItem>
          </list>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text:</para>
      <para>
        <superscript>1 </superscript>SQL Server 2008: Compliance  <externalLink><linkText>http://www.microsoft.com/sqlserver/2008/en/us/compliance.aspx</linkText><linkUri>http://www.microsoft.com/sqlserver/2008/en/us/compliance.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2 </superscript>Reaching Compliance: SQL Server 2008 Compliance Guide  <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2008/11/15/reaching-compliance-sql-server-2008-compliance-guide.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2008/11/15/reaching-compliance-sql-server-2008-compliance-guide.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> SQL Server 2008 R2 Books Online: Security Standards Compliance  <externalLink><linkText>http://msdn.microsoft.com/library/bb326717.aspx</linkText><linkUri>http://msdn.microsoft.com/library/bb326717.aspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>