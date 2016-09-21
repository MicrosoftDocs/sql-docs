---
title: "Failover Clustering (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f4356ab7-f414-492e-aa9d-d420ca8b019a
caps.latest.revision: 3
manager: jeffreyg
---
# Failover Clustering (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>Failover clustering is a popular solution to meet the high availability (HA) needs of mission-critical applications. It has been available for several releases of Windows Server and Microsoft SQL Server, and has been significantly improved in Windows Server 2008 and SQL Server 2008. Failover clustering uses a shared storage model and requires a storage area network (SAN). SQL Server failover clustering works well with other technologies such as database mirroring and log shipping. It is typically used to provide fault tolerance at the server level within a data center, but can also be extended to a remote data center—a solution referred to as stretch cluster, extended cluster, or geocluster.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide examples of customer scenarios for implementing failover clustering, in addition to general failover clustering reference material. (Note that the full URLs for the hyperlinked text are provided in the Appendix at the end of this document.)</para>
      <list class="bullet">
        <listItem>
          <para>The white paper <externalLink><linkText>SQL Server 2008 Failover Clustering</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2009/07/08/sql-server-2008-failover-clustering.aspx</linkUri></externalLink><superscript>1</superscript> covers failover cluster architecture and concepts for Windows Server 2003 and Windows Server 2008, in addition to SQL Server 2008. The white paper also covers installation of a SQL Server 2008 failover cluster, upgrades and updates to SQL Server 2008 failover clustering, and maintenance and administration of SQL Server 2008.</para>
        </listItem>
        <listItem>
          <para>The white paper <externalLink><linkText>Six Failover Clustering Benefits Realized from Migrating to SQL Server 2008</linkText><linkUri>http://sqlcat.com/top10lists/archive/2008/11/20/six-failover-clustering-benefits-realized-from-migrating-to-sql-server-2008.aspx</linkUri></externalLink><superscript>2</superscript> contains quick tips and benefits of failover clustering in SQL Server 2008. This paper lists the significant and immediate benefits of using SQL Server 2008 failover clustering.</para>
        </listItem>
        <listItem>
          <para>Unlike database mirroring, failover clustering supports distributed transactions. For more information, see the section "Microsoft Distributed Transaction Coordinator" in the white paper <externalLink><linkText>SQL Server 2008 Failover Clustering</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2009/07/08/sql-server-2008-failover-clustering.aspx</linkUri></externalLink>.<superscript>3</superscript></para>
        </listItem>
        <listItem>
          <para>Failover clustering has been improved in Windows Server 2008 and later versions, as described in the following articles. Note in particular though, that while Windows Server 2008 R2 clusters can be created across multiple subnets, SQL Server 2008 R2 requires cluster members to be part of the same subnet. When creating geoclusters, it is common to use virtual subnets to work around this limitation.</para>
          <list class="bullet">
            <listItem>
              <para>
                <externalLink>
                  <linkText>What's New in Failover Clusters in Windows Server 2008</linkText>
                  <linkUri>http://technet.microsoft.com/library/cc770625(WS.10).aspx</linkUri>
                </externalLink>
                <superscript>4</superscript>
              </para>
            </listItem>
            <listItem>
              <para>
                <externalLink>
                  <linkText>What's New in Failover Clusters</linkText>
                  <linkUri>http://technet.microsoft.com/library/dd443539(WS.10).aspx</linkUri>
                </externalLink>
                <superscript>5</superscript>
              </para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>By using volume mount points, you can mount a target partition onto a folder on another physical disk, as shown in the article <externalLink><linkText>How to configure volume mount points on a server cluster in Windows Server 2008</linkText><linkUri>http://support.microsoft.com/kb/947021</linkUri></externalLink>.<superscript>6</superscript> You can mitigate the limitation on the number of drive letters provided by the English alphabet through the use of volume mount points.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>SQL Server has successfully been deployed to achieve 4 and 5 nines by several customers. Example architectures are below: </para>
      <list class="bullet">
        <listItem>
          <para>The case study <externalLink><linkText>High Availability and Disaster Recovery at ServiceU: A SQL Server 2008 Technical Case Study</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2009/08/04/high-availability-and-disaster-recovery-at-serviceu-a-sql-server-2008-technical-case-study.aspx</linkUri></externalLink><superscript>7</superscript> provides a detailed description of the end-to-end HA and disaster recovery (DR) solution at ServiceU. ServiceU deploys failover clustering for local HA and database mirroring for DR.</para>
        </listItem>
        <listItem>
          <para>The case study <externalLink><linkText>SQL Server High Availability and Disaster Recovery for SAP Deployment at QR: A Technical Case Study</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2010/09/20/sql-server-high-availability-and-disaster-recovery-for-sap-deployment-at-qr-a-technical-case-study.aspx</linkUri></externalLink><superscript>8</superscript> provides a detailed description of a geocluster solution at Queensland Rail for a large scale SAP deployment.</para>
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
          <para>Understand prioritized HA/DR requirements for the application.</para>
        </listItem>
        <listItem>
          <para>Are your customers comfortable with a shared storage solution? </para>
          <list class="bullet">
            <listItem>
              <para>Failover clustering requires a shared storage solution, such as a SAN, and this means working closely with storage administrators.</para>
            </listItem>
            <listItem>
              <para>With shared storage, there is only one copy of data. The shared storage system needs to include redundancy to avoid being a single point of failure.</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>What is the RPO (Recovery Point Objective)? A zero data loss solution may require augmenting failover clustering with either synchronous database mirroring or synchronous SAN replication.</para>
        </listItem>
        <listItem>
          <para>What is the RTO (Recovery Time Objective)? A database mirroring HA solution may provide a better failover time than a failover clustering solution.</para>
        </listItem>
        <listItem>
          <para>Consider a geocluster (or stretch cluster) as a combined HA/DR solution. This solution requires software to enable the cluster and storage-level replication and from the storage vendor.</para>
        </listItem>
        <listItem>
          <para>Failover Clustering is often deployed along with database mirroring—clustering for local HA, and database mirroring for DR.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> SQL Server 2008 Failover Clustering  <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2009/07/08/sql-server-2008-failover-clustering.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2009/07/08/sql-server-2008-failover-clustering.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Six Failover Clustering Benefits Realized from Migrating to SQL Server 2008  <externalLink><linkText>http://sqlcat.com/top10lists/archive/2008/11/20/six-failover-clustering-benefits-realized-from-migrating-to-sql-server-2008.aspx</linkText><linkUri>http://sqlcat.com/top10lists/archive/2008/11/20/six-failover-clustering-benefits-realized-from-migrating-to-sql-server-2008.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> SQL Server 2008 Failover Clustering  <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2009/07/08/sql-server-2008-failover-clustering.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2009/07/08/sql-server-2008-failover-clustering.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> What's New in Failover Clusters in Windows Server 2008  <externalLink><linkText>http://technet.microsoft.com/library/cc770625(WS.10).aspx</linkText><linkUri>http://technet.microsoft.com/library/cc770625(WS.10).aspx)</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> What's New in Failover Clusters  <externalLink><linkText>http://technet.microsoft.com/library/dd443539(WS.10).aspx</linkText><linkUri>http://technet.microsoft.com/library/dd443539(WS.10).aspx)</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> How to configure volume mount points on a server cluster in Windows Server 2008  <externalLink><linkText>http://support.microsoft.com/kb/947021</linkText><linkUri>http://support.microsoft.com/kb/947021</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> High Availability and Disaster Recovery at ServiceU: A SQL Server 2008 Technical Case Study  <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2009/08/04/high-availability-and-disaster-recovery-at-serviceu-a-sql-server-2008-technical-case-study.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2009/08/04/high-availability-and-disaster-recovery-at-serviceu-a-sql-server-2008-technical-case-study.aspx</linkUri></externalLink></para>
      <para>
        <superscript>8</superscript> SQL Server High Availability and Disaster Recovery for SAP Deployment at QR: A Technical Case Study  <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2010/09/20/sql-server-high-availability-and-disaster-recovery-for-sap-deployment-at-qr-a-technical-case-study.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2010/09/20/sql-server-high-availability-and-disaster-recovery-for-sap-deployment-at-qr-a-technical-case-study.aspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>