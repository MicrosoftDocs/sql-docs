---
title: "Security Features (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 07c55a95-ff2f-48f1-bc21-1ce25c8734c2
caps.latest.revision: 4
manager: jeffreyg
---
# Security Features (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>There are many features that make up the security features of Microsoft SQL Server, including but not limited to auditing, policy-based management, and Transparent Data Encryption (TDE). But security of the database is only one aspect of securing the platform and the application as a whole. Security features beyond the database range from firewall, anti-malware, antivirus programs, and Windows security updates to end-to-end application auditing and packet sniffing. A very distinguishing capability is that Microsoft builds many (if not all) of these listed systems listed—the implication is that if we coordinate more closely with other teams, we can build more end-to-end secure environments by using each other’s mechanisms.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide some general information about compliance and security features.</para>
      <list class="bullet">
        <listItem>
          <para>Data warehouse applications often require special consideration when implementing security measures. A popular topic is <externalLink><linkText>Row Level Security</linkText><linkUri>http://vyaskn.tripod.com/row_level_security_in_sql_server_databases.htm</linkUri></externalLink>,<superscript>1</superscript> wherein a more complex layer of security beyond authentication is needed to restrict access to specific rows of data. This is an excellent step-by-step guide for how to set up row-level security in SQL Server.</para>
        </listItem>
        <listItem>
          <para>The web site <externalLink><linkText>SQL Server 2008: Compliance</linkText><linkUri>http://www.microsoft.com/sqlserver/2008/en/us/compliance.aspx</linkUri></externalLink><superscript>2</superscript> is the main site for information about SQL Server compliance, including an overview of governance. The following sections on the site are of particular interest:</para>
          <list class="bullet">
            <listItem>
              <para>Encrypting database data. Guidance and references for protecting sensitive data using encryption.</para>
            </listItem>
            <listItem>
              <para>Auditing sensitive information. Guidance and references for monitoring database events.</para>
            </listItem>
            <listItem>
              <para>Securing the platform. Guidance and references for securing the platform, end to end.</para>
            </listItem>
            <listItem>
              <para>Using policy-based management to define, deploy, and validate policies. Guidance and references for using policy-based management to address compliance requirements. </para>
            </listItem>
            <listItem>
              <para>Controlling identity and separation of duties. Guidance and references about the basics of identity and access control in addition to the policies surrounding the separation of duties.</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>The white paper <externalLink><linkText>Reaching Compliance: SQL Server 2008 Compliance </linkText><linkUri>http://sqlcat.com/whitepapers/archive/2008/11/15/reaching-compliance-sql-server-2008-compliance-guide.aspx</linkUri></externalLink><externalLink><linkText>Guide</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2008/11/15/reaching-compliance-sql-server-2008-compliance-guide.aspx</linkUri></externalLink><superscript>3</superscript> includes a deep dive into understanding compliance and its impact through regulatory requirements and organization policies.</para>
        </listItem>
        <listItem>
          <para>The <externalLink><linkText>Enterprise Policy Management Framework</linkText><linkUri>http://www.codeplex.com/EPMFramework</linkUri></externalLink><superscript>4</superscript> (EPM) is a CodePlex project that provides an end-to-end working framework for using SQL Server Policy-Based Management features to reach compliance goals. A key contribution of the EPM is that it allows the inclusion of SQL Server 2000 and 2005 servers into the framework.</para>
        </listItem>
        <listItem>
          <para>The <externalLink><linkText>Centralized Auditing Framework</linkText><linkUri>http://sqlcat.codeplex.com/wikipage?title=sqlauditcentral&amp;referringTitle=Home</linkUri></externalLink><superscript>5</superscript> is a CodePlex project that provides an end-to-end working framework for using SQL Server XEvents-based auditing feature to reach compliance goals.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>Industry standards compliance examples include:</para>
      <list class="bullet">
        <listItem>
          <para>PCI Compliance: </para>
          <list class="bullet">
            <listItem>
              <para>Customers have indicated that the white paper <externalLink><linkText>ParenteBeard: Deploying SQL Server 2008 Based on Payment Card Industry Data Security Standards (PCI DSS)</linkText><linkUri>http://www.parentebeard.com/Uploads/Files/Deploying_SQL_Server_2008_Based_on_PCI_DSS.PDF</linkUri></externalLink><superscript>6</superscript> is useful in providing developers and senior technology leaders with technical solutions on how to proactively achieve PCI compliance when deploying SQL Server 2008 to support and protect key business processes within an organization and avoid security and fraud risks. (If link does not open when clicked, copy the following URL into a browser. <externalLink><linkText>http://www.parentebeard.com/Uploads/Files/Deploying_SQL_Server_2008_Based_on_PCI_DSS.PDF</linkText><linkUri>http://www.parentebeard.com/Uploads/Files/Deploying_SQL_Server_2008_Based_on_PCI_DSS.PDF</linkUri></externalLink><externalLink><linkText>)</linkText><linkUri /></externalLink></para>
            </listItem>
            <listItem>
              <para>The webcast <externalLink><linkText>TechNet Webcast: SQL Server 2008 Capabilities for Meeting PCI Compliance Needs</linkText><linkUri>http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?culture=en-US&amp;EventID=1032404174&amp;CountryCode=US</linkUri></externalLink><superscript>7</superscript> provides insight into confirming PCI requirements are applied when implementing SQL Server 2008 technology.</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Health Insurance Portability and Accountability Act (HIPAA)/HealthAct Compliance:</para>
          <list class="bullet">
            <listItem>
              <para>The case study <externalLink><linkText>Beth Israel Deaconess Medical Center: Major Hospital Enhances Auditing Infrastructure using SQL Server 2008</linkText><linkUri>http://www.microsoft.com/canada/casestudies/Case_Study_Detail.aspx?casestudyid=4000003892</linkUri></externalLink><superscript>8</superscript> illustrates how Beth Israel Deaconess Medical Center was able to enhance its governance, risk management, and compliance efforts by upgrading to SQL Server 2008 Enterprise.</para>
            </listItem>
            <listItem>
              <para>The webcast <externalLink><linkText>TechNet Webcast: Supporting HIPAA Compliance with SQL Server 2008</linkText><linkUri>http://msevents.microsoft.com/CUI/EventDetail.aspx?EventID=1032441700&amp;Culture=en-US</linkUri></externalLink><superscript>9</superscript> provides insight into using SQL Server 2008 to meet HIPAA compliance requirements.</para>
            </listItem>
            <listItem>
              <para>Customers have indicated that they found the white paper <externalLink><linkText>Jefferson Wells: Supporting HIPAA Compliance with Microsoft SQL Server 2008</linkText><linkUri>http://www.jeffersonwells.com/mssql2008hipaa</linkUri></externalLink><superscript>10</superscript> useful in providing guidance on specific SQL Server 2008 features, and how they may be implemented to support the goals and technical safeguard considerations of HIPAA.</para>
            </listItem>
          </list>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <list class="bullet">
        <listItem>
          <para>Clearly understand how data warehouse security is implemented, especially with regards to the topic of row-level security.</para>
        </listItem>
        <listItem>
          <para>As noted in Governance #39, the regulations and organization policies ultimately help define what IT security features are deployed and required for tier-one enterprises to be compliant.</para>
        </listItem>
        <listItem>
          <para>Understanding compliance governance requirements allows you to determine the necessary IT features. It is important to research the specific local requirements in each location that the organization operates in.</para>
        </listItem>
        <listItem>
          <para>An important consideration is how does Microsoft get them to all work together? A potential solution is to work with outside vendors to provide end-to-end compliance solutions using SQL Server security features.</para>
        </listItem>
        <listItem>
          <para>Note that to truly secure the database, the entire platform must be secure.</para>
        </listItem>
        <listItem>
          <para>This leads to the question that will require Microsoft-wide executive commitment—since Microsoft creates the software and platforms for these systems end-to-end, shouldn’t we be able to create an end-to-end security solution for the entire platform? This would provide tier-one enterprises with a regulatory incentive to use the Microsoft stack.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text:</para>
      <para>
        <superscript>1 </superscript>Row Level Security <externalLink><linkText>http://vyaskn.tripod.com/row_level_security_in_sql_server_databases.htm</linkText><linkUri>http://vyaskn.tripod.com/row_level_security_in_sql_server_databases.htm</linkUri></externalLink></para>
      <para>
        <superscript>2 </superscript>SQL Server 2008: Compliance <externalLink><linkText>http://www.microsoft.com/sqlserver/2008/en/us/compliance.aspx</linkText><linkUri>http://www.microsoft.com/sqlserver/2008/en/us/compliance.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> Reaching Compliance: SQL Server 2008 Compliance Guide <externalLink><linkText>http://msdn.microsoft.com/library/bb326717.aspx</linkText><linkUri>http://msdn.microsoft.com/library/bb326717.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Enterprise Policy Management Framework <externalLink><linkText>http://www.codeplex.com/EPMFramework</linkText><linkUri>http://www.codeplex.com/EPMFramework</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Centralized Auditing Framework <externalLink><linkText>http://sqlcat.codeplex.com/wikipage?title=sqlauditcentral&amp;referringTitle=Home</linkText><linkUri>http://sqlcat.codeplex.com/wikipage?title=sqlauditcentral&amp;referringTitle=Home</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> ParenteBeard: Deploying SQL Server 2008 Based on Payment Card Industry Data Security Standards (PCI DSS). <externalLink><linkText>http://www.parentebeard.com/Uploads/Files/Deploying_SQL_Server_2008_Based_on_PCI_DSS.PDF</linkText><linkUri>http://www.parentebeard.com/Uploads/Files/Deploying_SQL_Server_2008_Based_on_PCI_DSS.PDF%20</linkUri></externalLink> (If link does not open when clicked, copy the following URL into a browser. <externalLink><linkText>http://www.parentebeard.com/Uploads/Files/Deploying_SQL_Server_2008_Based_on_PCI_DSS.PDF</linkText><linkUri>http://www.parentebeard.com/Uploads/Files/Deploying_SQL_Server_2008_Based_on_PCI_DSS.PDF</linkUri></externalLink>)</para>
      <para>
        <superscript>7</superscript> TechNet Webcast: SQL Server 2008 Capabilities for Meeting PCI Compliance Needs <externalLink><linkText>http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?culture=en-US&amp;EventID=1032404174&amp;CountryCode=US</linkText><linkUri>http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?culture=en-US&amp;EventID=1032404174&amp;CountryCode=US</linkUri></externalLink></para>
      <para>
        <superscript>8</superscript> Beth Israel Deaconess Medical Center: Major Hospital Enhances Auditing Infrastructure using SQL Server 2008 <externalLink><linkText>http://www.microsoft.com/canada/casestudies/Case_Study_Detail.aspx?casestudyid=4000003892</linkText><linkUri>http://www.microsoft.com/canada/casestudies/Case_Study_Detail.aspx?casestudyid=4000003892</linkUri></externalLink></para>
      <para>
        <superscript>9</superscript> TechNet Webcast: Supporting HIPAA Compliance with SQL Server 2008 <externalLink><linkText>http://msevents.microsoft.com/CUI/EventDetail.aspx?EventID=1032441700&amp;Culture=en-US</linkText><linkUri>http://msevents.microsoft.com/CUI/EventDetail.aspx?EventID=1032441700&amp;Culture=en-US</linkUri></externalLink></para>
      <para>
        <superscript>10</superscript> Jefferson Wells: Supporting HIPAA Compliance with Microsoft SQL Server 2008 <externalLink><linkText>http://www.jeffersonwells.com/mssql2008hipaa</linkText><linkUri>http://www.jeffersonwells.com/mssql2008hipaa</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>