$(document).ready(function () {
    $('input[name="ImageSource"]').change(function () {
        if (this.value == 'upload') {
            $('#imageFileGroup').show();
            $('#imageUrlGroup').hide();
        } else {
            $('#imageFileGroup').hide();
            $('#imageUrlGroup').show();
        }
    });

    $('input[name="EditImageSource"]').change(function () {
        if (this.value == 'upload') {
            $('#editImageFileGroup').show();
            $('#editImageUrlGroup').hide();
        } else {
            $('#editImageFileGroup').hide();
            $('#editImageUrlGroup').show();
        }
    });

    $('#saveComponentButton').click(function () {
        var form = $('#addComponentForm')[0];
        var formData = new FormData(form);

        $.ajax({
            url: '@Url.Action("Create", "Component")',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.success) {
                    location.reload(); // Reload the page to see the new component
                } else {
                    alert('An error occurred while adding the component.');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('An error occurred: ' + textStatus);
            }
        });
    });

    $('.edit-button').click(function () {
        var id = $(this).data('id');
        $.ajax({
            url: '@Url.Action("Edit", "Component")/' + id,
            type: 'GET',
            success: function (data) {
                $('#editComponentID').val(data.ComponentID);
                $('#EditName').val(data.Name);
                $('#EditType').val(data.Type);
                $('#EditManufacturer').val(data.Manufacturer);
                $('#EditPrice').val(data.Price);
                if (data.ImagePath) {
                    $('#editUploadOption').prop('checked', true);
                    $('#editImageFileGroup').show();
                    $('#editImageUrlGroup').hide();
                } else {
                    $('#editUrlOption').prop('checked', true);
                    $('#editImageFileGroup').hide();
                    $('#editImageUrlGroup').show();
                }
                $('#editComponentModal').modal('show');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('An error occurred: ' + textStatus);
            }
        });
    });

    $('#updateComponentButton').click(function () {
        var form = $('#editComponentForm')[0];
        var formData = new FormData(form);
        var id = $('#editComponentID').val();

        $.ajax({
            url: '@Url.Action("Update", "Component")/' + id,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.success) {
                    $('#editComponentModal').modal('hide'); // Close the modal
                    location.reload(); // Reload the page to see the updated component
                } else {
                    alert('An error occurred while updating the component.');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('An error occurred: ' + textStatus);
            }
        });
    });

    $('.delete-button').click(function () {
        var id = $(this).data('id');
        var name = $(this).data('name');
        var manufacturer = $(this).data('manufacturer')
        $('#deleteComponentID').val(id);
        $('#deleteComponentMessage').text(`${manufacturer} ${name}`);
        $('#deleteComponentModal').modal('show');
    });

    $('#confirmDeleteButton').click(function () {
        var form = $('#deleteComponentForm')[0];
        var formData = new FormData(form);
        var id = $('#deleteComponentID').val();

        $.ajax({
            url: '@Url.Action("DeleteConfirmed", "Component")/' + id,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.success) {
                    $('#deleteComponentModal').modal('hide'); // Close the modal
                    location.reload(); // Reload the page to see the updated component list
                } else {
                    alert('An error occurred while deleting the component.');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('An error occurred: ' + textStatus);
            }
        });
    });
});