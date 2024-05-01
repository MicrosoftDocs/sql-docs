---
title: Configure multiple-subnet availability group & FCI (Linux)
description: Learn to configure multiple-subnet Always On availability groups and failover cluster instances (FCI) for SQL Server on Linux.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: vanto, randolphwest
ms.date: 01/31/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---

# Configure multiple-subnet Always On availability groups and failover cluster instances

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

When an Always On availability group (AG) or failover cluster instance (FCI) spans more than one site, each site usually has its own networking, which often means that each site has its own IP addressing.

For example, the Site A addresses start with `192.168.1.<x>`, and the Site B addresses start with `192.168.2.<x>`, where `<x>` is the part of the IP address that is unique to the server. Without some sort of routing in place at the networking layer, these servers aren't able to communicate with each other.

There are two ways to handle this scenario:

- set up a network that bridges the two different subnets (known as a VLAN)
- configure routing between the subnets

## VLAN-based solution

**Prerequisite**: For a VLAN-based solution, each server participating in an AG or FCI needs two network cards (NICs) for proper availability (a dual port NIC would be a single point of failure on a physical server), so that it can be assigned IP addresses on its native subnet as well as one on the VLAN. This requirement is in addition to any other network needs, such as iSCSI, which also needs its own network.

The IP address creation for the AG or FCI is done on the VLAN. In the following example, the VLAN has a subnet of `192.168.3.<x>`, so the IP address created for the AG or FCI is `192.168.3.104`. Nothing additional needs to be configured, since there's a single IP address assigned to the AG or FCI.

:::image type="content" source="media/sql-server-linux-configure-multiple-subnet/image1.png" alt-text="Diagram showing how to configure multiple subnets with VLAN.":::

## Configuration with Pacemaker

On Windows, a Windows Server Failover Cluster (WSFC) natively supports multiple subnets and handles multiple IP addresses via an OR dependency on the IP address. On Linux, there's no OR dependency, but there's a way to achieve a proper multi-subnet natively with Pacemaker, though you can't use the normal Pacemaker command line. Instead, you need to modify the cluster information base (CIB). The CIB is an XML file with the Pacemaker configuration.

:::image type="content" source="media/sql-server-linux-configure-multiple-subnet/image2.png" alt-text="Diagram showing how to configure multiple subnets with Pacemaker.":::

### Update the CIB

##### [Red Hat Enterprise Linux (RHEL)](#tab/rhel)

1. Export the CIB.

   ```bash
   sudo pcs cluster cib <filename>
   ```

   Where `<filename>` is the name you want to call the CIB.

1. Edit the file that was generated. Look for the `<resources>` section. You see the various resources that were created for the AG or FCI. Find the one associated with the IP address. Add a `<instance_attributes>` section with the information for the second IP address either before or after the existing one, but before `<operations>`. It's similar to the following syntax:

   ```xml
   <instance_attributes id="<NameForAttribute>">
       <nvpair id="<NameForIP>" name="ip" value="<IPAddress>"/>
   </instance_attributes>
   ```

   where `<NameForAttribute>` is the unique name for this attribute, `<NameForIP>` is the name associated with the IP address, `<IPAddress>` is the IP address for the second subnet.

   The following shows an example.

   ```xml
   <instance_attributes id="virtualip-instance_attributes">
       <nvpair id="virtualip-instance_attributes-ip" name="ip" value="192.168.1.102"/>
   </instance_attributes>
   ```

   By default, there's only one `<instance_attributes />` in the CIB XML file exported. If there are two subnets, you need two `<instance_attributes />` entries.

   Here's an example of entries for two subnets:

   ```xml
   <instance_attributes id="virtualip-instance_attributes1">
       <rule id="Subnet1-IP" score="INFINITY" boolean-op="or">
           <expression id="Subnet1-Node1" attribute="#uname" operation="eq" value="Node1" />
           <expression id="Subnet1-Node2" attribute="#uname" operation="eq" value="Node2" />
       </rule>
       <nvpair id="IP-In-Subnet1" name="ip" value="192.168.1.102"/>
   </instance_attributes>
   <instance_attributes id="virtualip-instance_attributes2">
       <rule id="Subnet2-IP" score="INFINITY">
           <expression id="Subnet2-Node1" attribute="#uname" operation="eq" value="Node3" />
       </rule>
       <nvpair id="IP-In-Subnet2" name="ip" value="192.168.2.102"/>
   </instance_attributes>
   ```

   You would use `boolean-op="or"` when the subnet has more than one server.

1. Import the modified CIB and reconfigure Pacemaker.

   ```bash
   sudo pcs cluster cib-push <filename>
   ```

   Where `<filename>` is the name of the CIB file with the modified IP address information.

##### [SUSE Linux Enterprise Server (SLES)](#tab/sles)

1. Export the CIB.

   ```bash
   sudo cibadmin -Q > <filename>
   ```

   Where `<filename>` is the name you want to call the CIB.

1. Edit the file that was generated. Look for the `<resources>` section. You see the various resources that were created for the AG or FCI. Find the one associated with the IP address. Add a `<instance_attributes>` section with the information for the second IP address either before or after the existing one, but before `<operations>`. It's similar to the following syntax:

   ```xml
   <instance_attributes id="<NameForAttribute>">
       <nvpair id="<NameForIP>" name="ip" value="<IPAddress>"/>
   </instance_attributes>
   ```

   where `<NameForAttribute>` is the unique name for this attribute, `<NameForIP>` is the name associated with the IP address, `<IPAddress>` is the IP address for the second subnet.

   The following shows an example.

   ```xml
   <instance_attributes id="virtualip-instance_attributes">
       <nvpair id="virtualip-instance_attributes-ip" name="ip" value="192.168.1.102"/>
   </instance_attributes>
   ```

   By default, there's only one `<instance_attributes />` in the CIB XML file exported. If there are two subnets, you need two `<instance_attributes />` entries.

   Here's an example of entries for two subnets:

   ```xml
   <instance_attributes id="virtualip-instance_attributes1">
       <rule id="Subnet1-IP" score="INFINITY" boolean-op="or">
           <expression id="Subnet1-Node1" attribute="#uname" operation="eq" value="Node1" />
           <expression id="Subnet1-Node2" attribute="#uname" operation="eq" value="Node2" />
       </rule>
       <nvpair id="IP-In-Subnet1" name="ip" value="192.168.1.102"/>
   </instance_attributes>
   <instance_attributes id="virtualip-instance_attributes2">
       <rule id="Subnet2-IP" score="INFINITY">
           <expression id="Subnet2-Node1" attribute="#uname" operation="eq" value="Node3" />
       </rule>
       <nvpair id="IP-In-Subnet2" name="ip" value="192.168.2.102"/>
   </instance_attributes>
   ```

   You would use `boolean-op="or"` when the subnet has more than one server.

1. Import the modified CIB and reconfigure Pacemaker.

   ```bash
   sudo cibadmin -R -x <filename>
   ```

   Where `<filename>` is the name of the CIB file with the modified IP address information.

##### [Ubuntu](#tab/ubuntu)

1. Export the CIB.

   ```bash
   sudo pcs cluster cib <filename>
   ```

   Where `<filename>` is the name you want to call the CIB.

1. Edit the file that was generated. Look for the `<resources>` section. You see the various resources that were created for the AG or FCI. Find the one associated with the IP address. Add a `<instance_attributes>` section with the information for the second IP address either before or after the existing one, but before `<operations>`. It's similar to the following syntax:

   ```xml
   <instance_attributes id="<NameForAttribute>">
       <nvpair id="<NameForIP>" name="ip" value="<IPAddress>"/>
   </instance_attributes>
   ```

   where `<NameForAttribute>` is the unique name for this attribute, `<NameForIP>` is the name associated with the IP address, `<IPAddress>` is the IP address for the second subnet.

   The following shows an example.

   ```xml
   <instance_attributes id="virtualip-instance_attributes">
       <nvpair id="virtualip-instance_attributes-ip" name="ip" value="192.168.1.102"/>
   </instance_attributes>
   ```

   By default, there's only one `<instance_attributes />` in the CIB XML file exported. If there are two subnets, you need two `<instance_attributes />` entries.

   Here's an example of entries for two subnets:

   ```xml
   <instance_attributes id="virtualip-instance_attributes1">
       <rule id="Subnet1-IP" score="INFINITY" boolean-op="or">
           <expression id="Subnet1-Node1" attribute="#uname" operation="eq" value="Node1" />
           <expression id="Subnet1-Node2" attribute="#uname" operation="eq" value="Node2" />
       </rule>
       <nvpair id="IP-In-Subnet1" name="ip" value="192.168.1.102"/>
   </instance_attributes>
   <instance_attributes id="virtualip-instance_attributes2">
       <rule id="Subnet2-IP" score="INFINITY">
           <expression id="Subnet2-Node1" attribute="#uname" operation="eq" value="Node3" />
       </rule>
       <nvpair id="IP-In-Subnet2" name="ip" value="192.168.2.102"/>
   </instance_attributes>
   ```

   You would use `boolean-op="or"` when the subnet has more than one server.

1. Import the modified CIB and reconfigure Pacemaker.

   ```bash
   sudo pcs cluster cib-push <filename>
   ```

   Where `<filename>` is the name of the CIB file with the modified IP address information.

---

### Check and verify failover

1. After the CIB is successfully applied with the updated configuration, ping the DNS name associated with the IP address resource in Pacemaker. It should reflect the IP address associated with the subnet currently hosting the AG or FCI.

1. Fail the AG or FCI to the other subnet.

1. After the AG or FCI is fully online, ping the DNS name associated with the IP address. It should reflect the IP address in the second subnet.

1. If desired, fail the AG or FCI back to the original subnet.

## Related content

- [Configure multiple-subnet Always On Availability Group by modifying CIB](https://techcommunity.microsoft.com/t5/sql-server-support-blog/configure-multiple-subnet-alwayson-availability-group-by/ba-p/1544838)
