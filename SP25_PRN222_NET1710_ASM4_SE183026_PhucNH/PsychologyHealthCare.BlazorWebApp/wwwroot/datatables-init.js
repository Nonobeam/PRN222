console.log("datatables-init.js loaded");
window.initializeDataTable = () => {
    setTimeout(() => {
        // Kiểm tra nếu DataTable đã được khởi tạo, thì hủy nó trước khi tạo mới
        if ($.fn.DataTable.isDataTable("#indexTable")) {
            $('#indexTable').DataTable().destroy();
        }
        // Khởi tạo lại DataTable
        let table = new DataTable('#indexTable', {
            paging: true,
            pageLength: 5,
            lengthChange: false,
            info: false,
            ordering: true,
            searching: true,
            order: [],
            columnDefs: [
                { targets: 9, orderable: false }
            ]
        });
        console.log("datatables-init.js initialized successfully");
    }, 10000); // Delay 2000ms to ensure the table has rendered
}