<html>
<head>
<title> Test by Animesh </title>
 
 
 
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.2.1/css/buttons.dataTables.min.css"/>
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css"/>
 
<link rel="stylesheet" type="text/css" href="DataTables-1.10.12/css/jquery.dataTables.css"/>
<link rel="stylesheet" type="text/css" href="AutoFill-2.1.2/css/autoFill.dataTables.min.css"/>
<link rel="stylesheet" type="text/css" href="ColReorder-1.3.2/css/colReorder.dataTables.css"/>
<link rel="stylesheet" type="text/css" href="FixedColumns-3.2.2/css/fixedColumns.dataTables.css"/>
<link rel="stylesheet" type="text/css" href="FixedHeader-3.1.2/css/fixedHeader.dataTables.css"/>
<link rel="stylesheet" type="text/css" href="KeyTable-2.1.2/css/keyTable.dataTables.css"/>
<link rel="stylesheet" type="text/css" href="Responsive-2.1.0/css/responsive.dataTables.css"/>
<link rel="stylesheet" type="text/css" href="RowReorder-1.1.2/css/rowReorder.dataTables.css"/>
<link rel="stylesheet" type="text/css" href="Scroller-1.4.2/css/scroller.dataTables.css"/>
<link rel="stylesheet" type="text/css" href="Select-1.2.0/css/select.dataTables.css"/>
 
 
 
<script type="text/javascript" src="//code.jquery.com/jquery-1.12.3.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.1/js/dataTables.buttons.min.js"></script>
<script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
<script type="text/javascript" src="/cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/pdfmake.min.js"></script>
<script type="text/javascript" src="//cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/vfs_fonts.js"> </script>
<script type="text/javascript" src="//cdn.datatables.net/buttons/1.2.1/js/buttons.html5.min.js"></script>
<script type="text/javascript" src="//cdn.datatables.net/buttons/1.2.1/js/buttons.colVis.min.js"></script>
 
<script type="text/javascript" src="jQuery-2.2.3/jquery-2.2.3.js"></script> -->
<script type="text/javascript" src="DataTables-1.10.12/js/jquery.dataTables.js"></script>
<script type="text/javascript" src="AutoFill-2.1.2/js/dataTables.autoFill.js"></script>
<script type="text/javascript" src="ColReorder-1.3.2/js/dataTables.colReorder.js"></script>
<script type="text/javascript" src="FixedColumns-3.2.2/js/dataTables.fixedColumns.js"></script>
<script type="text/javascript" src="FixedHeader-3.1.2/js/dataTables.fixedHeader.js"></script>
<script type="text/javascript" src="KeyTable-2.1.2/js/dataTables.keyTable.js"></script>
<script type="text/javascript" src="Responsive-2.1.0/js/dataTables.responsive.js"></script>
<script type="text/javascript" src="RowReorder-1.1.2/js/dataTables.rowReorder.js"></script>
<script type="text/javascript" src="Scroller-1.4.2/js/dataTables.scroller.js"></script>
<script type="text/javascript" src="Select-1.2.0/js/dataTables.select.js"></script>
</style>
 
 
 
<script type="text/javascript" language="javascript">
$(document).ready(function() {
    $('#example').DataTable( {
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [ 0, ':visible' ]
                }
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [ 0, 1, 2, 5 ]
                }
            },
            'colvis'
        ]
    } );
} );
 
 
function fnShowHide( iCol )
{
    var oTable = $('#example').dataTable();
 
    var bVis = oTable.fnSettings().aoColumns[iCol].bVisible;
    oTable.fnSetColumnVis( iCol, bVis ? false : true );
}
 
</script>
 
</head>
<body class="dt-example">
 
 
<!-- dt-example-semanticui" -->
    <div class="container" id="container" >
        <section>
            <h1>DataTables example <span>Semantic UI (Tech. preview)</span></h1>
            <div class="info">
                <p>This example is Animesh's Example.</p>
            </div>
            <table id="example" class="display" cellspacing="0" width="100%" source='data.csv'>
 
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Position</th>
                        <th>Office</th>
                        <th>Age</th>
                        <th>Start date</th>
                        <th>Salary</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th>Name</th>
                        <th>Position</th>
                        <th>Office</th>
                        <th>Age</th>
                        <th>Start date</th>
                        <th>Salary</th>
                    </tr>
                </tfoot>
                <tbody>
                    <tr>
                        <td>Tiger Nixon</td>
                        <td>System Architect</td>
                        <td>Edinburgh</td>
                        <td>61</td>
                        <td>2011/04/25</td>
                        <td>$320,800</td>
                    </tr>
                    <tr>
                        <td>Garrett Winters</td>
                        <td>Accountant</td>
                        <td>Tokyo</td>
                        <td>63</td>
                        <td>2011/07/25</td>
                        <td>$170,750</td>
                    </tr>
                    <tr>
                        <td>Ashton Cox</td>
                        <td>Junior Technical Author</td>
                        <td>San Francisco</td>
                        <td>66</td>
                        <td>2009/01/12</td>
                        <td>$86,000</td>
                    </tr>
                    <tr>
                        <td>Cedric Kelly</td>
                        <td>Senior Javascript Developer</td>
                        <td>Edinburgh</td>
                        <td>22</td>
                        <td>2012/03/29</td>
                        <td>$433,060</td>
                    </tr>
                    </tbody>
                </table>
 
 
</section>
 
 
    </div>
    </section>
</body>
</html>