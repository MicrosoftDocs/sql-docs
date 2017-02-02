CallableStatement cstmt = con.prepareCall("{call employee_jobid (?, ?)}");
cstmt.registerOutParameter(2, java.sql.Types.SMALLINT);
String lname = "Brown";
cstmt.setString(1, lname);
Resultset rs = cstmt.executeQuery();
short empJobID = cstmt.getShort(2);
rs.close();
cstmt.close();