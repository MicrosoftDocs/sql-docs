---
title: "Compliance (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 0e7b473b-f4ef-4f77-9e74-e76e31ea309c
caps.latest.revision: 4
manager: jeffreyg
---
# Compliance (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
<?xml version="1.0" encoding="utf-8"?>
<developerConceptualDocument xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://ddue.schemas.microsoft.com/authoring/2003/5 http://clixdevr3.blob.core.windows.net/ddueschema/developer.xsd">
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
  </introduction>
  <section>
    <title>Key Concepts</title>
    <content>
      <para>Many of the tier-one applications that customers deploy are required to follow government or business regulations. For example, in the U.S.A., healthcare organizations are required to follow Health Insurance Portability and Accountability Act (HIPAA) and HealthAct regulations, while financial organizations are required to follow payment card industry (PCI) and Sarbanes-Oxley (SOX) regulations. Failing to comply with these regulations can be very expensive. At a minimum, each failure to comply incurs financial penalties. For example, with the HealthAct regulations, each perceived breach of patient medical data incurs punitive penalties. Large-scale compliance failures result in negative publicity and the loss of trust in a company. For example, 15,500 Northern California Kaiser patients had their medical records breached in December 2009 causing unpleasant publicity.</para>
      <para>Data warehouse applications fall under unique implementation guidelines in terms of how a data warehouse is used for analytical and reporting purposes. Since a data warehouse will often contain more than just raw data, such as a "complete picture" of a patient or a customer account for example, it is imperative that information be carefully protected.</para>
    </content>
  </section>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide some general information about compliance. (Note that the full URLs for the hyperlinked text are provided in the Appendix at the end of this document.)</para>
      <list class="bullet">
        <listItem>
          <para>The white paper, <externalLink><linkText>Reaching Compliance: SQL Server 2008 Compliance</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2008/11/15/reaching-compliance-sql-server-2008-compliance-guide.aspx</linkUri></externalLink><superscript>1</superscript>, provides comprehensive guidance for governance, risk management, and compliance (GRC). The white paper also provides policy guidance and information about the application of IT features and sample scripts to reach these goals.. </para>
        </listItem>
        <listItem>
          <para>The <externalLink><linkText>Enterprise Policy Management Framework</linkText><linkUri>http://www.codeplex.com/EPMFramework</linkUri></externalLink><superscript>2</superscript> (EPM) is a CodePlex project that provides an end-to-end working framework for using Microsoft SQL Server Policy-Based Management features to reach compliance goals. A key contribution of the EPM is that it allows the inclusion of SQL Server 2000 and 2005 servers into the framework.</para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Centralized Auditing Framework</linkText>
              <linkUri>http://sqlcat.codeplex.com/wikipage?title=sqlauditcentral&amp;referringTitle=Home</linkUri>
            </externalLink>
            <superscript>3</superscript> is a CodePlex project that provides an end-to-end working framework for using SQL Server’s XEvents-based Auditing feature to reach compliance goals.</para>
        </listItem>
        <listItem>
          <para>FIPS standards are developed jointly by the National Institute of Standards and Technology (NIST) in the U.S.A. and the Communications Security Establishment (CSE) in Canada. The Microsoft Support article, “<externalLink><linkText>Instructions for using SQL Server 2008 in FIPS 140-2-compliant</linkText><linkUri>http://support.microsoft.com/kb/955720</linkUri></externalLink><superscript>4</superscript>,” discusses Federal Information Processing Standard (FIPS) 140-2 instructions and describes how to use SQL Server 2008 in FIPS 140-2 compliant mode.</para>
        </listItem>
        <listItem>
          <para>The Microsoft Software Developer Network (MSDN) Webcast, "<externalLink><linkText>Addressing Compliance with SQL Server 2008</linkText><linkUri>http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?EventID=1032389193&amp;EventCategory=5&amp;culture=en-US&amp;CountryCode=US</linkUri></externalLink><superscript>5</superscript>,” provides details on compliance concepts, including identity management, data protection, separation of duties, auditing and reporting, and policy-based management using SQL Server 2008.</para>
        </listItem>
        <listItem>
          <para>Consider using partitioned tables for large extraction, transformation, and load (ETL) operations. Loading data to an empty partition, creating the indexes, and switching the partition into the table can be faster than normal insert operations by orders of magnitude. For more information, see the following articles:</para>
          <para>Corporate Integrity is a strategy advisory firm that researches governance, risk, and compliance issues. Some customers have mentioned that they find the research documentation listed on their site, “<externalLink><linkText>Written Research</linkText><linkUri>http://www.corp-integrity.com/written-research</linkUri></externalLink><superscript>6</superscript>,” useful.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>Industry standards compliance examples and implementation guidelines include:</para>
      <list class="bullet">
        <listItem>
          <para>PCI Compliance:</para>
          <list class="bullet">
            <listItem>
              <para>The white paper, “<externalLink><linkText>ParenteBeard: Deploying SQL Server 2008 Based on Payment Card Industry Data Security Standards (PCI DSS)</linkText><linkUri>http://www.parentebeard.com/Uploads/Files/Deploying_SQL_Server_2008_Based_on_PCI_DSS.PDF</linkUri></externalLink><superscript>7</superscript>,”  provides developers and senior technology leaders with technical solutions on how to proactively achieve PCI compliance when deploying SQL Server 2008 to support and protect key business processes within an organization and avoid security and fraud risks.</para>
            </listItem>
            <listItem>
              <para>The webcast, “<externalLink><linkText>TechNet Webcast: SQL Server 2008 Capabilities for Meeting PCI Compliance Needs</linkText><linkUri>http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?culture=en-US&amp;EventID=1032404174&amp;CountryCode=US</linkUri></externalLink><superscript>8</superscript>,” provides insight into meeting PCI requirements with SQL Server 2008 technology.</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>HIPAA/HealthAct Compliance:</para>
          <list class="bullet">
            <listItem>
              <para>The case study, “<externalLink><linkText>Beth Israel Deaconess Medical Center: Major Hospital Enhances Auditing Infrastructure using SQL Server 2008</linkText><linkUri>http://www.microsoft.com/canada/casestudies/Case_Study_Detail.aspx?casestudyid=4000003892</linkUri></externalLink><superscript>9</superscript>,” illustrates how Beth Israel Deaconess Medical Center was able to enhance its governance, risk management, and compliance efforts by upgrading to SQL Server 2008 Enterprise.</para>
            </listItem>
            <listItem>
              <para>The webcast, “<externalLink><linkText>TechNet Webcast: Supporting HIPAA Compliance with SQL Server 2008</linkText><linkUri>http://msevents.microsoft.com/CUI/EventDetail.aspx?EventID=1032441700&amp;Culture=en-US</linkUri></externalLink><superscript>10</superscript>,” provides insight into using SQL Server 2008 to meet HIPAA compliance requirements.</para>
            </listItem>
            <listItem>
              <para>Some customers have found the white paper, “<externalLink><linkText>Jefferson Wells: Supporting HIPAA Compliance with Microsoft SQL Server 2008</linkText><linkUri>http://www.jeffersonwells.com/mssql2008hipaa</linkUri></externalLink><superscript>11</superscript>,” on the Jefferson Wells website useful as it provides guidance on specific SQL Server 2008 features, and how they may be implemented to support the goals and technical safeguard considerations of HIPAA.</para>
            </listItem>
          </list>
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
          <para>Understand the government regulations and compliance requirements by working with external partners to develop best practices and technical guidance associated with the regulatory guidance. In particular, seek to establish the requirements for the country/region in which the organization is operating.</para>
        </listItem>
        <listItem>
          <para>Understand which data points are required for full auditing across different regulations.</para>
        </listItem>
        <listItem>
          <para>Understand the security and encryption requirements across different regulations (for example, private/public key at the field level, and column-based encryption).</para>
        </listItem>
        <listItem>
          <para>Understand how different organizations manage their data and comply with their own internal policies, which are often a superset of the standard regulations.</para>
        </listItem>
        <listItem>
          <para>Most importantly, understand the minimal working set of IT features required to meet all of these regulations and policies. If you use fewer technologies, the solution is potentially simpler.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> "Reaching Compliance: SQL Server 2008 Compliance Guide,"<externalLink><linkText>http://sqlcat.com/whitepapers/archive/2008/11/15/reaching-compliance-sql-server-2008-compliance-guide.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2008/11/15/reaching-compliance-sql-server-2008-compliance-guide.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> "Enterprise Policy Management Framework," <externalLink><linkText>http://www.codeplex.com/EPMFramework</linkText><linkUri>http://www.codeplex.com/EPMFramework</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> "Centralized Auditing Framework," <externalLink><linkText>http://sqlcat.codeplex.com/wikipage?title=sqlauditcentral&amp;referringTitle=Home</linkText><linkUri>http://sqlcat.codeplex.com/wikipage?title=sqlauditcentral&amp;referringTitle=Home</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> "Instructions for using SQL Server 2008 in FIPS 140-2-compliant mode," <externalLink><linkText>http://support.microsoft.com/kb/955720</linkText><linkUri>http://support.microsoft.com/kb/955720</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> "MSDN Webcast: Addressing Compliance with SQL Server 2008," <externalLink><linkText>http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?EventID=1032389193&amp;EventCategory=5&amp;culture=en-US&amp;CountryCode=US</linkText><linkUri>http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?EventID=1032389193&amp;EventCategory=5&amp;culture=en-US&amp;CountryCode=US</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> "Written Research" (at the Corporate Integrity website), <externalLink><linkText>http://www.corp-integrity.com/written-research</linkText><linkUri>http://www.corp-integrity.com/written-research</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> "ParenteBeard: Deploying SQL Server 2008 Based on Payment Card Industry Data Security Standards (PCI DSS)," <externalLink><linkText>http://www.parentebeard.com/Uploads/Files/Deploying_SQL_Server_2008_Based_on_PCI_DSS.PDF</linkText><linkUri>http://www.parentebeard.com/Uploads/Files/Deploying_SQL_Server_2008_Based_on_PCI_DSS.PDF</linkUri></externalLink></para>
      <para>
        <superscript>8</superscript> "TechNet Webcast: SQL Server 2008 Capabilities for Meeting PCI Compliance Needs," <externalLink><linkText>http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?culture=en-US&amp;EventID=1032404174&amp;CountryCode=US</linkText><linkUri>http://msevents.microsoft.com/CUI/WebCastEventDetails.aspx?culture=en-US&amp;EventID=1032404174&amp;CountryCode=US</linkUri></externalLink></para>
      <para>
        <superscript>9</superscript> "Beth Israel Deaconess Medical Center: Major Hospital Enhances Auditing Infrastructure using SQL Server 2008,” <externalLink><linkText>http://www.microsoft.com/canada/casestudies/Case_Study_Detail.aspx?casestudyid=4000003892</linkText><linkUri>http://www.microsoft.com/canada/casestudies/Case_Study_Detail.aspx?casestudyid=4000003892</linkUri></externalLink></para>
      <para>
        <superscript>10</superscript> "TechNet Webcast: Supporting HIPAA Compliance with SQL Server 2008," <externalLink><linkText>http://msevents.microsoft.com/CUI/EventDetail.aspx?EventID=1032441700&amp;Culture=en-US</linkText><linkUri>http://msevents.microsoft.com/CUI/EventDetail.aspx?EventID=1032441700&amp;Culture=en-US</linkUri></externalLink></para>
      <para>
        <superscript>11</superscript> "Jefferson Wells: Supporting HIPAA Compliance with Microsoft SQL Server 2008," <externalLink><linkText>http://www.jeffersonwells.com/mssql2008hipaa</linkText><linkUri>http://www.jeffersonwells.com/mssql2008hipaa</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>