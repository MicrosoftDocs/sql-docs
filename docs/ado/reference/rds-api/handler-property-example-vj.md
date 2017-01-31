---
$title: "Handler Property Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Handler property [ADO], VJ++ example"
ms.assetid: 21e85d66-37db-4ce1-ad24-4344f43cebde
caps.latest.revision: 14
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# Handler Property Example (VJ++)
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/en-us/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](http://go.microsoft.com/fwlink/?LinkId=199565).  
  
 This example demonstrates the [RDS DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object [Handler](../../../ado/reference/rds-api/handler-property-rds.md) property. (See [DataFactory Customization](../../../ado/guide/remote-data-service/datafactory-customization.md) for more details.)  
  
 Assume the following sections in the parameter file, Msdfmap.ini, located on the server:  
  
```  
[connect AuthorDataBase]  
Access=ReadWrite  
Connect="DSN=Pubs"  
[sql AuthorById]  
SQL="SELECT * FROM Authors WHERE au_id = ?"  
```  
  
 Your code looks like the following. The command assigned to the [SQL](../../../ado/reference/rds-api/sql-property.md) property will match the *AuthorById* identifier and will retrieve a row for author Michael O'Leary. Although the [Connect](../../../ado/reference/rds-api/connect-property-rds.md) property in your code specifies the Northwind data source, that data source will be overwritten by the Msdfmap.ini *connect* section. The **DataControl** object's [Recordset](../../../ado/reference/rds-api/recordset-sourcerecordset-properties-rds.md) property is assigned to a disconnected [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object purely as a coding convenience.  
  
```  
// BeginHandlerJ  
import com.ms.wfc.data.*;  
import com.ms.wfc.data.rds.*;  
import java.io.* ;  
  
public class HandlerX  
{  
   // The main entry point for the application.  
  
   public static void main (String[] args)  
   {  
      HandlerX();  
      System.exit(0);  
   }  
  
   // HandlerX function  
  
   static void HandlerX()  
   {  
  
      // Define ADO Objects.  
      Recordset rstAuthors = null;  
  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader(System.in));  
      int intCount = 0;  
      int intDisplaysize = 15;  
  
      try  
      {  
         IBindMgr dc = (IBindMgr) new DataControl();  
         dc.setServer("MyServer");  
         dc.setConnect("Data Source=Northwind");  
         dc.setSQL("AuthorById(267-41-2394)");  
         dc.Refresh();                  // Retrieve the record.  
         // Use another recordset as a convenience.  
         rstAuthors = (Recordset)dc.getRecordset();  
         System.out.println("Author is '" +  
                        rstAuthors.getField("au_fname").getString() +  
                        " " +  
                        rstAuthors.getField("au_lname").getString() +  
                        "'");  
  
         System.out.println("\nPress <Enter> to continue..");  
         in.readLine();  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // As passing a Recordset, check for null pointer first.  
         if (rstAuthors != null)  
         {  
            PrintProviderError(rstAuthors.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
      }  
  
      // System read requires this catch.  
      catch( java.io.IOException je)  
      {  
         PrintIOError(je);  
      }  
      catch(java.lang.UnsatisfiedLinkError e)  
      {  
         System.out.println("Exception: " + e.getMessage());  
      }  
      catch(java.lang.NullPointerException ne)  
      {  
         System.out.println(  
         "Exception: Attempt to use null where an object is required.");  
      }     
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstAuthors != null)  
            if (rstAuthors.getState() == 1)  
               rstAuthors.close();  
      }  
   }  
  
   // PrintProviderError Function  
  
   static void PrintProviderError( Connection Cnn1 )  
   {  
      // Print Provider errors from Connection object.  
      // ErrItem is an item object in the Connections Errors collection.  
      com.ms.wfc.data.Error  ErrItem = null;  
      long nCount = 0;  
      int  i      = 0;  
  
      nCount = Cnn1.getErrors().getCount();  
  
      // If there are any errors in the collection, print them.  
      if( nCount > 0);  
      {  
         // Collection ranges from 0 to nCount - 1  
         for (i = 0; i< nCount; i++)  
         {  
            ErrItem = Cnn1.getErrors().getItem(i);  
            System.out.println("\t Error number: " + ErrItem.getNumber()  
               + "\t" + ErrItem.getDescription() );  
         }  
      }  
  
   }  
  
   // PrintIOError Function  
  
   static void PrintIOError( java.io.IOException je)  
   {  
      System.out.println("Error \n");  
      System.out.println("\tSource = " + je.getClass() + "\n");  
      System.out.println("\tDescription = " + je.getMessage() + "\n");  
   }  
}  
// EndHandlerJ  
```  
  
## See Also  
 [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)   
 [Handler Property (RDS)](../../../ado/reference/rds-api/handler-property-rds.md)


