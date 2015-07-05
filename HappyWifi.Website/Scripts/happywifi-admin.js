'use strict';

function ImagesController() {
    this.HandleEvents = function () {
        $('.add-image-btn').click(function () {
            var imageid = $(this).attr('data-imageid');

            $('#shared-modal-template .modal-title').html('<span>ADD</span>');
            var htmlbody = $('#add-image-template').html();
            var htmlFooter = '<button class="btn btn-primary add-image-confirm">Save Changes</button><button class="btn btn-default" data-dismiss="modal">Cancel</button>';
            $('#shared-modal-template .modal-body').html(htmlbody);
            $('#shared-modal-template .modal-footer').html(htmlFooter);
            $('#shared-modal-template').modal('show');
        });

        $('.edit-image-btn').click(function () {
            var imageid = $(this).attr('data-imageid');

            $('#shared-modal-template .modal-title').html('<span>EDIT</span>');
            var htmlbody = $('#negotiatenow-modal-template').html();
            var htmlFooter = '<button class="btn btn-primary">Save Changes</button><button class="btn btn-default" data-dismiss="modal">Cancel</button>';
            $('#shared-modal-template .modal-body').html(htmlbody);
            $('#shared-modal-template .modal-footer').html(htmlFooter);
            $('#shared-modal-template').modal('show');
        });

        $('.hide-image-btn').click(function () {
            var imageid = $(this).attr('data-imageid');

            $('#shared-modal-template .modal-title').html('<span>HIDE</span>');
            var htmlFooter = '<button class="btn btn-primary hide-image-confirm" data-imageid="' + imageid + '">OK</button><button class="btn btn-default" data-dismiss="modal">Cancel</button>';
            $('#shared-modal-template .modal-body').html('<div class="col-md-12"><p>Are you sure?</p></div>');
            $('#shared-modal-template .modal-footer').html(htmlFooter);
            $('#shared-modal-template').modal('show');
        });

        $('.show-image-btn').click(function () {
            var imageid = $(this).attr('data-imageid');

            $('#shared-modal-template .modal-title').html('<span>Show</span>');
            var htmlFooter = '<button class="btn btn-primary show-image-confirm" data-imageid="' + imageid + '">OK</button><button class="btn btn-default" data-dismiss="modal">Cancel</button>';
            $('#shared-modal-template .modal-body').html('<div class="col-md-12"><p>Are you sure?</p></div>');
            $('#shared-modal-template .modal-footer').html(htmlFooter);
            $('#shared-modal-template').modal('show');
        });

        $('.delete-image-btn').click(function () {
            var imageid = $(this).attr('data-imageid');

            $('#shared-modal-template .modal-title').html('<span>DELETE</span>');
            var htmlFooter = '<button class="btn btn-primary delete-image-confirm" data-imageid="' + imageid + '">OK</button><button class="btn btn-default" data-dismiss="modal">Cancel</button>';
            $('#shared-modal-template .modal-body').html('<div class="col-md-12"><p>Are you sure?</p></div>');
            $('#shared-modal-template .modal-footer').html(htmlFooter);
            $('#shared-modal-template').modal('show');
        });

        //send http requests
        $('#shared-modal-template').on('click', '.delete-image-confirm', function () {
            var imageid = $(this).attr('data-imageid');
            var apiURL = '/api/v1/file/delete/' + imageid;
            //send ajax request to toogle follow
            var ajaxRequest = $.ajax({
                type: "POST",
                url: apiURL,
                contentType: false,
                processData: false,
            });

            ajaxRequest.done(function (data) {
                $('#shared-modal-template').modal('hide');
                alert('Success');
                RealoadPage();
            });

            ajaxRequest.fail(function () {
                alert("Something went wrong, Please try again.");
            });

            //ajaxRequest.always(function () {

            //});
        });

        $('#shared-modal-template').on('click', '.hide-image-confirm', function () {
            var imageid = $(this).attr('data-imageid');
            var apiURL = '/api/v1/file/Update/' + imageid + '?IsHidden=true';
            //send ajax request to toogle follow
            var ajaxRequest = $.ajax({
                type: "POST",
                url: apiURL,
                contentType: false,
                processData: false,
            });

            ajaxRequest.done(function (data) {
                $('#shared-modal-template').modal('hide');
                alert('Success');
                RealoadPage();
            });

            ajaxRequest.fail(function () {
                alert("Something went wrong, Please try again.");
            });

            //ajaxRequest.always(function () {

            //});
        });

        $('#shared-modal-template').on('click', '.show-image-confirm', function () {
            var imageid = $(this).attr('data-imageid');
            var apiURL = '/api/v1/file/Update/' + imageid + '?IsHidden=false';
            //send ajax request to toogle follow
            var ajaxRequest = $.ajax({
                type: "POST",
                url: apiURL,
                contentType: false,
                processData: false,
            });

            ajaxRequest.done(function (data) {
                $('#shared-modal-template').modal('hide');
                alert('Success');
                RealoadPage();
            });

            ajaxRequest.fail(function () {
                alert("Something went wrong, Please try again.");
            });

            //ajaxRequest.always(function () {

            //});
        });

        $('#shared-modal-template').on('click', '.add-image-confirm', function () {
            var imageid = $(this).attr('data-imageid');
            var apiURL = '/api/v1/file/upload/';
            var params = $('#shared-modal-template #add-image-form').serialize();
            var formdata = new FormData($("#shared-modal-template #add-image-form")[0]);

            //send ajax request to toogle follow
            var ajaxRequest = $.ajax({
                type: "POST",
                url: apiURL,
                contentType: false,
                processData: false,
                data: formdata
            });

            ajaxRequest.done(function (data) {
                $('#shared-modal-template').modal('hide');
                alert('Success');
                RealoadPage();
            });

            ajaxRequest.fail(function () {
                alert("Something went wrong, Please try again.");
            });

            //ajaxRequest.always(function () {

            //});
        });
    }

    function RealoadPage() {
        console.log('reload:' + window.location.href);
        window.location = window.location.href;
    }
}

$(function () {
    var imagesController = new ImagesController();
    imagesController.HandleEvents();
});