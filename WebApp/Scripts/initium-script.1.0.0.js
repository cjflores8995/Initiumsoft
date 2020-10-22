class InitiumSoft {

	OnBeginJs() {
		Dashmix.layout('header_loader_on');

		//disabled btn-call-event
		$('.btn-call-event').attr('disabled', true);
	}

	OnSuccessJs(data) {

		if (data.Status) {
			$('#s-a-m-text').text(data.Message);
			jQuery('#success-alert-message').toast('show');

			setTimeout(() => {
				window.location.href = data.Details;
			}, 2000);
		}
		else {
			$('#s-a-m-text-err').text(data.Message);
			jQuery('#error-alert-message').toast('show');
        }

		Dashmix.layout('header_loader_off');


	}

	OnFailureJs() {
		Dashmix.layout('header_loader_off');
	}

	OnCompleteJs() {
		console.log('acción finalizada correctamente.');
	}
}

var initiumSoft = new InitiumSoft();