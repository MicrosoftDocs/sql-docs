---
title: "Version Upgrade (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 07b5ce3c-e375-4806-9f5b-843b42729e93
caps.latest.revision: 5
manager: jeffreyg
---
# Version Upgrade (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
<?xml version="1.0" encoding="utf-8"?>
<developerConceptualDocument xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://ddue.schemas.microsoft.com/authoring/2003/5 http://clixdevr3.blob.core.windows.net/ddueschema/developer.xsd">
  <introduction>
    <table xmlns:caps="http://schemas.microsoft.com/build/caps/2013/11">
      <tbody>
        <tr>
          <TD>
            <para>
              <embeddedLabel>Want more guides like this one?</embeddedLabel> Go to <externalLink><linkText>Technical Reference Guides for Designing Mission-Critical Solutions</linkText><linkUri>http://technet.microsoft.com/sqlserver/hh273157</linkUri></externalLink>.</para>
          </TD>
        </tr>
      </tbody>
    </table>
    <para>Data warehouse version upgrades usually involve additional tables and/or table structure changes, such as new/deleted columns or new partitioning or indexes. Additionally, upgrades to the presentation layer are involved. Whenever version upgrades are introduced, they can have a significant impact on all aspects of the data warehouse, including data loads, user reports, downstream systems, backup/restore strategies, data security, end-user queries, and so on.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following section provides some advice and examples of customer scenarios for upgrading Microsoft SQL Server; also listed are general SQL Server upgrade reference materials.</para>
      <list class="bullet">
        <listItem>
          <para>Downtime and rollback procedure. There is usually an associated downtime with the upgrade process. Some of this can be limited by upgrading multiple components at a time or utilizing another instance of SQL Server to do the upgrade (references below).</para>
        </listItem>
        <listItem>
          <para>The <externalLink><linkText>Microsoft SQL Server Upgrade Advisor</linkText><linkUri>http://www.microsoft.com/downloads/en/details.aspx?familyid=F5A6C5E9-4CD9-4E42-A21C-7291E7F0F852&amp;displaylang=en</linkUri></externalLink><superscript>1</superscript> is utility customers can use to help evaluate their environment for potential issues during an upgrade. </para>
        </listItem>
        <listItem>
          <para>For list of version upgrade best practices, use the <externalLink><linkText>SQL Server 2008 Upgrade Technical Reference Guide</linkText><linkUri>http://www.microsoft.com/downloads/en/details.aspx?FamilyID=66d3e6f5-6902-4fdd-af75-9975aea5bea7&amp;displaylang=en</linkUri></externalLink>.<superscript>2</superscript></para>
        </listItem>
        <listItem>
          <para>In a failover cluster environment, there is no rolling Windows upgrade path from Windows Server 2003 to Windows Server 2008 or from Windows Server 2008 to Windows Server 2008 R2. This can have a significant effect on the SQL Server availability in a cluster upgrade or migration scenario. This issue is described in the Microsoft Support article, <externalLink><linkText>You cannot upgrade the operating system of a clustered server from Windows Server 2003 to Windows Server 2008 or Windows Server 2008 R2 and from Windows Server 2008 to Windows Server 2008 R2</linkText><linkUri>http://support.microsoft.com/kb/935197</linkUri></externalLink>.<superscript>3</superscript></para>
        </listItem>
        <listItem>
          <para>Hyper-V/Live Migration can also be used to minimize the impact to SQL Server/the Hyper-V instance, in terms of a Windows upgrade of a host system.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>The following samples can be used for reference.</para>
      <list class="bullet">
        <listItem>
          <para>The article, <externalLink><linkText>How to: Minimize Downtime for Mirrored Databases When Upgrading Server Instances</linkText><linkUri>http://msdn.microsoft.com/library/bb677181.aspx</linkUri></externalLink><superscript>4</superscript> describes the rolling upgrade procedure using database mirroring.</para>
        </listItem>
        <listItem>
          <para>The article, <externalLink><linkText>High Availability and Disaster Recovery at ServiceU: A SQL Server 2008 Technical Case Study</linkText><linkUri>http://msdn.microsoft.com/library/ee355221.aspx</linkUri></externalLink><superscript>5</superscript> demonstrates how Windows failover clustering and SQL Server 2008 database mirroring can help eliminate single points of failure in data centers and enable fast recovery from a possible disaster. </para>
        </listItem>
        <listItem>
          <para>Windows maintenance and security patching can also have a large effect on the uptime of a SQL Server. For a sample architecture to help avoid downtime in this scenario, see the CareGroup example in slides 21-26 of the <externalLink><linkText>Proven Customer Deployed Architectures and Scenarios for SQL Server HA/DR.</linkText><linkUri>http://ecn.channel9.msdn.com/o9/te/NorthAmerica/2010/pptx/DAT401.pptx</linkUri></externalLink><superscript>6</superscript></para>
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
          <para>Most tier-one data warehouses will have a rigid set of guidelines for handling intrusive upgrades, but many do not, and one should carefully understand and plan accordingly.</para>
        </listItem>
        <listItem>
          <para>It is imperative that mission-critical or very large database (VLDB) data warehouses have a test-and-development environment on which all upgrades are thoroughly tested before deploying to production.</para>
        </listItem>
        <listItem>
          <para>Evaluate features and the need to upgrade. There is usually a trade-off or evaluation process in terms of need for a new feature included in the version upgrade versus time to test, sign off, and deploy into production.</para>
        </listItem>
        <listItem>
          <para>New product versions have an impact on the supportability of previous versions of the product, as explained in the <externalLink><linkText>Microsoft Support Lifecycle</linkText><linkUri>http://support.microsoft.com/lifecycle/</linkUri></externalLink><superscript>7</superscript> website.</para>
        </listItem>
        <listItem>
          <para>Windows upgrades can also have a major impact on maintenance and uptime for SQL Server. This can be a significant impact to SQL Server running on Windows Server.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> Microsoft SQL Server Upgrade Advisor  <externalLink><linkText>http:/</linkText><linkUri>http://www.microsoft.com/downloads/en/details.aspx?familyid=F5A6C5E9-4CD9-4E42-A21C-7291E7F0F852&amp;displaylang=en</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> SQL Server 2008 Upgrade Technical Reference Guide <externalLink><linkText>http:/</linkText><linkUri>http://www.microsoft.com/downloads/en/details.aspx?FamilyID=66d3e6f5-6902-4fdd-af75-9975aea5bea7&amp;displaylang=en</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> You cannot upgrade the operating system of a clustered server from Windows Server 2003 to Windows Server 2008 or Windows Server 2008 R2 and from Windows Server 2008 to Windows Server 2008 R2  <externalLink><linkText>http://support.microsoft.com/kb/935197</linkText><linkUri>http://support.microsoft.com/kb/935197</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> How to: Minimize Downtime for Mirrored Databases When Upgrading Server Instances  <externalLink><linkText>http://msdn.microsoft.com/library/bb677181.aspx</linkText><linkUri>http://msdn.microsoft.com/library/bb677181.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> High Availability and Disaster Recovery at ServiceU: A SQL Server 2008 Technical Case Study  <externalLink><linkText>http://msdn.microsoft.com/library/ee355221.aspx</linkText><linkUri>http://msdn.microsoft.com/library/ee355221.aspx</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> Proven Customer Deployed Architectures and Scenarios for SQL Server HA/DR  <externalLink><linkText>http://ecn.channel9.msdn.com/o9/te/NorthAmerica/2010/pptx/DAT401.pptx</linkText><linkUri>http://ecn.channel9.msdn.com/o9/te/NorthAmerica/2010/pptx/DAT401.pptx</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> Microsoft Support Lifecycle  <externalLink><linkText>http://support.microsoft.com/lifecycle/#tab0</linkText><linkUri>http://support.microsoft.com/lifecycle/</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>