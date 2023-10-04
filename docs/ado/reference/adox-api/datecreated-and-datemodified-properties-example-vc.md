---
title: "DateCreated and DateModified Properties Example (VC++)"
description: "DateCreated and DateModified Properties Example (VC++)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "DateCreated property [ADOX], VC++ example"
  - "DateModified property [ADOX], VC++ example"
dev_langs:
  - "C++"
---
# DateCreated and DateModified Properties Example (VC++)
This example demonstrates the [DateCreated](./datecreated-property-adox.md) and [DateModified](./datemodified-property-adox.md) properties by adding a new [Column](./column-object-adox.md) to an existing [Table](./table-object-adox.md) and by creating a new **Table**. The DateOutput procedure is required for this example to run.  
  
```  
// BeginDateCreatedCpp.cpp  
// compile with: /EHsc  
#import "msado15.dll" rename("EOF", "EndOfFile")  
#import "msadox.dll" no_namespace  
  
#include "iostream"  
using namespace std;  
  
// Function declarations  
inline void TESTHR(HRESULT x) {if FAILED(x) _com_issue_error(x);};  
void DateCreatedX();  
void DateOutPut(_bstr_t strTemp, _TablePtr tblTemp);  
  
int main() {  
    if ( FAILED(::CoInitialize(NULL) ) )  
        return -1;  
    DateCreatedX();  
    ::CoUninitialize();  
}  
  
void DateCreatedX() {  
    HRESULT hr = S_OK;  
  
    // Define ADOX object pointers, initialize pointers. These are in ADODB  namespace.  
    _CatalogPtr m_pCatalog = NULL;  
    _TablePtr m_pTblEmployees = NULL;  
    _TablePtr m_pTblNew = NULL;  
  
    // Set ActiveConnection of Catalog to this string  
    _bstr_t strCnn("Provider='Microsoft.JET.OLEDB.4.0';Data Source= 'c:\\Northwind.mdb';");  
  
    try {  
        TESTHR(hr = m_pCatalog.CreateInstance(__uuidof (Catalog)));  
  
        // Connect the catalog.  
        m_pCatalog->PutActiveConnection(strCnn);  
        m_pTblEmployees = m_pCatalog->Tables->GetItem("Employees");  
  
        // Print current information about the Employees table.  
        DateOutPut((_bstr_t)"Current properties", m_pTblEmployees);  
  
        // Create and append column to the Employees table.  
        m_pTblEmployees->Columns->Append("NewColumn", adInteger,0);  
        m_pCatalog->Tables->Refresh();  
  
        // Print new information about the Employees table.  
        DateOutPut((_bstr_t)"After creating a new column", m_pTblEmployees);  
  
        // Delete new column because this is a demonstration.  
        m_pTblEmployees->Columns->Delete("NewColumn");  
  
        // Create and append new Table object to the Northwind database.  
        TESTHR(hr = m_pTblNew.CreateInstance(__uuidof(Table)));  
  
        m_pTblNew->Name = "NewTable";  
        m_pTblNew->Columns->Append("NewColumn", adInteger,0);  
        m_pCatalog->Tables->Append(_variant_t((IDispatch*)m_pTblNew));  
        m_pCatalog->Tables->Refresh();  
  
        // Print information about the new Table object.  
        DateOutPut((_bstr_t)"After creating a new table", m_pCatalog->Tables->GetItem("NewTable"));  
  
        // Delete new Table object because this is a demonstration.  
        m_pCatalog->Tables->Delete(m_pTblNew->Name);  
    }  
  
    catch(_com_error &e) {  
        // Notify the user of errors if any.  
        _bstr_t bstrSource(e.Source());  
        _bstr_t bstrDescription(e.Description());  
  
        printf("\n\tSource :  %s \n\tdescription : %s \n ", (LPCSTR)bstrSource, (LPCSTR)bstrDescription);  
    }  
  
    catch(...) {  
        cout << "Error occurred in include files...." << endl;  
    }  
}  
  
void DateOutPut(_bstr_t strTemp , _TablePtr tblTemp) {  
    // Print DateCreated and DateModified information about specified Table object.  
    cout << strTemp << endl;  
    cout << "    Table: " << tblTemp->GetName() << endl;  
    cout << "        DateCreated = " << (_bstr_t)tblTemp->GetDateCreated() << endl;  
    cout << "        DateModified = " << (_bstr_t)tblTemp->GetDateModified() << endl;  
}  
```  
  
## See Also  
 [Column Object (ADOX)](./column-object-adox.md)   
 [DateCreated Property (ADOX)](./datecreated-property-adox.md)   
 [DateModified Property (ADOX)](./datemodified-property-adox.md)   
 [Table Object (ADOX)](./table-object-adox.md)