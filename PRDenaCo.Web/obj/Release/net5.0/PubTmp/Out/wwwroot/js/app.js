'use strict';

(function ($) {

	var wind_ = $(window),
		body_ = $('body');

	/*------------- create/remove overlay -------------*/
	$.createOverlay = function () {
		if ($('.overlay').length < 1) {
			body_.addClass('no-scroll').append('<div class="overlay"></div>');
			$('.overlay').addClass('show');
		}
	};

	$.removeOverlay = function () {
		body_.removeClass('no-scroll');
		$('.overlay').remove();
	};
	/*------------- create/remove overlay -------------*/

	$('[data-backround-image]').each(function (e) {
		$(this).css("background", 'url(' + $(this).data('backround-image') + ')');
	});

	/*------------- page loader -------------*/
	wind_.on('load', function () {
		$('.page-loader').fadeOut(700);

		$(document).on("keypress", ".only-float", function (e) {

			if (!checkValidFloat(e, $(this).val(), $(this).data('min'), $(this).data('max')))
				e.preventDefault();
		});

		$(document).on('paste', '.only-float', function (e) {
			e.preventDefault();

			try {
				var value = parseFloat(e.originalEvent.clipboardData.getData('text'));
				$(this).val(value);
			}
			catch (error) { console.log(error); }
		});

		$(document).on("keypress", ".only-integer", function (e) {

			if (!checkValidInteger(e, $(this).val(), $(this).data('min'), $(this).data('max')))
				e.preventDefault();
		});

		$(document).on('paste', '.only-integer', function (e) {
			e.preventDefault();

			try {
				let value = parseInt(e.originalEvent.clipboardData.getData('text'));

				if (value.trim() != 'NaN' && value.toString().length <= 15) {
					$(this).val(value);
				}
			}
			catch (error) { }
		});

	});
	/*------------- page loader -------------*/

	/*------------- side menu (sub menü arrow) -------------*/
	wind_.on('load', function () {
		setTimeout(function () {
			$('.navigation .navigation-menu-body ul li a').each(function () {
				var $this = $(this);
				if ($this.next('ul').length) {
					if (body_.hasClass('horizontal-side-menu')) {
						$this.append('<i class="sub-menu-arrow ti-angle-right"></i>');
						$('.navigation .navigation-menu-body > ul > li > a > .sub-menu-arrow').removeClass('ti-angle-right').addClass('ti-angle-down');
					} else {
						$this.append('<i class="sub-menu-arrow ti-plus"></i>');
					}
				}
			});
			$('.navigation .navigation-menu-body ul li.open>a>.sub-menu-arrow').removeClass('ti-plus').addClass('ti-minus').addClass('rotate-in');
		}, 200);
	});

	$(document).on('click', '.navigation .navigation-icon-menu ul li a', function (e) {
		if (!$(this).hasClass('go-to-page')) {
			e.preventDefault();
			$(this).parent().tooltip('hide');
			var target = $(this).attr('href');
			$(this).closest('ul').find('li').removeClass('active');
			$(this).parent('li').addClass('active');
			$('.navigation .navigation-menu-body ul.navigation-active').removeClass('navigation-active');
			$('.navigation .navigation-menu-body ul' + target).addClass('navigation-active');
			return false;
		}
	});
	/*------------- side menu (sub menü arrow) -------------*/

	$.fn.modal.Constructor.prototype.enforceFocus = function () {
		modal_this = this
		$(document).on('focusin.modal', function (e) {
			if (modal_this.$element[0] !== e.target && !modal_this.$element.has(e.target).length
				// add whatever conditions you need here:
				&&
				!$(e.target.parentNode).hasClass('cke_dialog_ui_input_select') && !$(e.target.parentNode).hasClass('cke_dialog_ui_input_text')) {
				modal_this.$element.focus()
			}
		})
	};

	$(document).on('click', '.navbar-toggler', function () {
		$('.header .header-body ul.navbar-nav').toggleClass('open');
		return false;
	});

	$(document).on('click', '.navigation-toggler', function () {
		$('.navigation').toggleClass('open');
		$.createOverlay();
		return false;
	});

	$(document).on('click', '*', function (e) {
		if (!$(e.target).is('.header .header-body ul.navbar-nav, .header .header-body ul.navbar-nav *, .navbar-toggler, .navbar-toggler *')) {
			$('.header .header-body ul.navbar-nav').removeClass('open');
		}
	});

	/*------------- sidebar show/hide -------------*/
	$(document).on('click', '[data-sidebar-open]', function () {
		$('[data-toggle="dropdown"]').dropdown('hide');
		$(this).tooltip('hide');
		var sidebar_id = $(this).data('sidebar-open');
		$('.sidebar').removeClass('show');
		$(sidebar_id).addClass('show');
		$.createOverlay();
		return false;
	});

	$(document).on('click', '.overlay', function () {
		$('.sidebar').removeClass('show');
		$('.navigation').removeClass('open');
		$.removeOverlay();
	});

	/*------------- mobile or hidden side menu open -------------*/
	$(document).on('click', '.side-menu-open', function () {
		$('[data-toggle="dropdown"]').dropdown('hide');
		$('.navigation').addClass('show');
		$.createOverlay();
		return false;
	});
	/*------------- mobile or hidden side menu open -------------*/

	/*------------- form validation -------------*/
	window.addEventListener('load', function () {
		// Fetch all the forms we want to apply custom Bootstrap validation styles to
		var forms = document.getElementsByClassName('needs-validation');
		// Loop over them and prevent submission
		Array.prototype.filter.call(forms, function (form) {
			form.addEventListener('submit', function (event) {
				if (form.checkValidity() === false) {
					event.preventDefault();
					event.stopPropagation();
				}
				form.classList.add('was-validated');
			}, false);
		});
	}, false);
	/*------------- form validation -------------*/

	/*------------- responsive html table -------------*/
	var table_responsive_stack = $(".table-responsive-stack");
	table_responsive_stack
		.find("th")
		.each(function (i) {
			$(".table-responsive-stack td:nth-child(" + (i + 1) + ")").prepend(
				'<span class="table-responsive-stack-thead">' +
				$(this).text() +
				":</span> "
			);
			$(".table-responsive-stack-thead").hide();
		});

	table_responsive_stack.each(function () {
		var thCount = $(this).find("th").length,
			rowGrow = 100 / thCount + "%";
		$(this).find("th, td").css("flex-basis", rowGrow);
	});

	function flexTable() {
		if (wind_.width() < 768) {
			$(".table-responsive-stack").each(function (i) {
				$(this)
					.find(".table-responsive-stack-thead")
					.show();
				$(this)
					.find("thead")
					.hide();
			});

			// window is less than 768px
		} else {
			$(".table-responsive-stack").each(function (i) {
				$(this)
					.find(".table-responsive-stack-thead")
					.hide();
				$(this)
					.find("thead")
					.show();
			});
		}
	}

	flexTable();
	initCustomScrollbar();

	window.onresize = function (event) {
		flexTable();
		initCustomScrollbar('resize');
	};
	/*------------- responsive html table -------------*/

	/*------------- custom accordion -------------*/
	$(document).on('click', '.accordion.custom-accordion .accordion-row a.accordion-header', function () {
		var $this = $(this);
		$this.closest('.accordion.custom-accordion').find('.accordion-row').not($this.parent()).removeClass('open');
		$this.parent('.accordion-row').toggleClass('open');
		return false;
	});
	/*------------- custom accordion -------------*/

	/*------------- responsive table dropdown -------------*/
	var dropdownMenu,
		table_responsive = $('.table-responsive');

	table_responsive.on('show.bs.dropdown', function (e) {
		dropdownMenu = $(e.target).find('.dropdown-menu');
		body_.append(dropdownMenu.detach());
		var eOffset = $(e.target).offset();
		dropdownMenu.css({
			'display': 'block',
			'top': eOffset.top + $(e.target).outerHeight(),
			'left': eOffset.left,
			'width': '184px',
			'font-size': '14px'
		});
		dropdownMenu.addClass("mobPosDropdown");
	});

	table_responsive.on('hide.bs.dropdown', function (e) {
		$(e.target).append(dropdownMenu.detach());
		dropdownMenu.hide();
	});
	/*------------- responsive table dropdown -------------*/

	/*------------- chat -------------*/
	$(document).on('click', '.chat-app-wrapper .btn-chat-sidebar-open', function () {
		$('.chat-app-wrapper .chat-sidebar').addClass('chat-sidebar-opened');
		return false;
	});

	$(document).on('click', '*', function (e) {
		if (!$(e.target).is('.chat-app-wrapper .chat-sidebar, .chat-app-wrapper .chat-sidebar *, .chat-app-wrapper .btn-chat-sidebar-open, .chat-app-wrapper .btn-chat-sidebar-open *')) {
			$('.chat-app-wrapper .chat-sidebar').removeClass('chat-sidebar-opened');
		}
	});
	/*------------- chat -------------*/

	/*------------- aside menu toggle -------------*/
	$(document).on('click', '.navigation ul li a', function () {
		var $this = $(this);
		if ($this.next('ul').length) {
			var sub_menu_arrow = $this.find('.sub-menu-arrow');
			sub_menu_arrow.toggleClass('rotate-in');
			$this.next('ul').toggle(200);
			$this.parent('li').siblings().find('ul').not($this.parent('li').find('ul')).slideUp(200);
			$this.next('ul').find('li ul').slideUp(200);
			$this.next('ul').find('li>a').find('.sub-menu-arrow').removeClass('ti-minus').addClass('ti-plus');
			$this.next('ul').find('li>a').find('.sub-menu-arrow').removeClass('rotate-in');
			$this.parent('li').siblings().not($this.parent('li').find('ul')).find('>a').find('.sub-menu-arrow').removeClass('ti-minus').addClass('ti-plus');
			$this.parent('li').siblings().not($this.parent('li').find('ul')).find('>a').find('.sub-menu-arrow').removeClass('rotate-in');
			if (sub_menu_arrow.hasClass('rotate-in')) {
				setTimeout(function () {
					sub_menu_arrow.removeClass('ti-plus').addClass('ti-minus');
				}, 200);
			} else {
				sub_menu_arrow.removeClass('ti-minus').addClass('ti-plus');
			}
			if (!body_.hasClass('horizontal-side-menu') && wind_.width() >= 768) {
				setTimeout(function (e) {
					$('.navigation>.navigation-menu-body>ul').getNiceScroll().resize();
				}, 300);
			}
			return false;
		}
	});

	$('body.icon-side-menu .navigation').hover(function (e) {}, function (e) {
		e.stopPropagation();
		$('.navigation ul').removeAttr('style');
		$('.navigation ul li').not('.open').find('>a>.sub-menu-arrow').removeClass('rotate-in').removeClass('ti-minus').addClass('ti-plus');
	});
	/*------------- aside menu toggle -------------*/

	/*------------- other -------------*/
	$(document).on('click', '.dropdown-menu', function (e) {
		e.stopPropagation();
	});

	$('#exampleModal').on('show.bs.modal', function (event) {
		var button = $(event.relatedTarget),
			recipient = button.data('whatever'),
			modal = $(this);

		modal.find('.modal-title').text('New message to ' + recipient);
		modal.find('.modal-body input').val(recipient);
	});

	$('[data-toggle="tooltip"]').tooltip();

	$('[data-toggle="popover"]').popover();

	$('.carousel').carousel();

	if (body_.hasClass('icon-side-menu')) {
		$('.navigation').hover(function (e) {
			$('.navigation').on('webkitTransitionEnd otransitionend oTransitionEnd msTransitionEnd transitionend', function (e) {
				$('.navigation>.navigation-menu-body>ul').niceScroll();
			});
		});
	}

	function initCustomScrollbar(type) {

		type = type ? type : '';

		if (type == 'resize') {
			if (wind_.width() >= 768) {
				$('.card-scroll').getNiceScroll().resize();
			}

			if (
				(!body_.hasClass('horizontal-side-menu') && wind_.width() >= 992) ||
				body_.hasClass('horizontal-side-menu') && wind_.width() < 992
			) {
				if (wind_.width() >= 768) {
					$('.navigation>.navigation-menu-body>ul').getNiceScroll().resize();
				}
			}

			$('.card').each(function () {
				if (wind_.width() >= 768) {
					var $this = $(this),
						scroll_div = $this.find('.card-scroll');
					if (scroll_div.length) {
						scroll_div.getNiceScroll().resize();
					}
				}
			});

			$('.sidebar').each(function () {
				if (wind_.width() >= 768) {
					var $this = $(this);
					$this.getNiceScroll().resize();
				}
			});

			$('.dropdown-menu').each(function () {
				if (typeof $('.dropdown-menu-body', this)[0] != 'undefined' && wind_.width() >= 768) {
					$('.dropdown-menu-body', this).getNiceScroll().resize();
				}
			});

			if (wind_.width() >= 768) {
				$('.chat-app .chat-sidebar .chat-sidebar-messages')[0] ? $('.chat-app .chat-sidebar .chat-sidebar-messages').getNiceScroll().resize() : '';

				$('.chat-app .chat-body .chat-body-messages')[0] ? $('.chat-app .chat-body .chat-body-messages').getNiceScroll().resize() : '';
			}

		} else {
			if (wind_.width() >= 768) {
				$('.card-scroll').niceScroll();
				$('.table-responsive').niceScroll();
			}

			if (
				(!body_.hasClass('horizontal-side-menu') && wind_.width() >= 992) ||
				body_.hasClass('horizontal-side-menu') && wind_.width() < 992
			) {
				wind_.on('load', function () {
					if (!body_.hasClass('horizontal-side-menu') && !body_.hasClass('icon-side-menu') && wind_.width() >= 768) {
						$('.navigation>.navigation-menu-body>ul').niceScroll();
					}
				});
			}

			$('.card').each(function () {
				if (wind_.width() >= 768) {
					var $this = $(this),
						scroll_div = $this.find('.card-scroll');
					if (scroll_div.length) {
						scroll_div.niceScroll();
					}
				}
			});

			$('.sidebar').each(function () {
				if (wind_.width() >= 768) {
					var $this = $(this);
					$this.niceScroll();
				}
			});

			$('.dropdown-menu').each(function () {
				if (typeof $('.dropdown-menu-body', this)[0] != 'undefined' && wind_.width() >= 768) {
					$('.dropdown-menu-body', this).niceScroll();
				}
			});

			if (wind_.width() >= 768) {
				$('.chat-app .chat-sidebar .chat-sidebar-messages')[0] ? $('.chat-app .chat-sidebar .chat-sidebar-messages').scrollTop($('.chat-app .chat-sidebar .chat-sidebar-messages').get(0).scrollHeight, -1).niceScroll() : '';

				$('.chat-app .chat-body .chat-body-messages')[0] ? $('.chat-app .chat-body .chat-body-messages').scrollTop($('.chat-app .chat-body .chat-body-messages').get(0).scrollHeight, -1).niceScroll() : '';
			}
		}
	}

	if (typeof CKEDITOR == 'object' && $('body').hasClass('dark')) {
		var backgroundColor = $('.card').css("background-color"),
			fontColor = $('body').css("color");
		CKEDITOR.on('instanceReady', function (e) {
			var iframe = $('iframe.cke_wysiwyg_frame');
			iframe.each(function (e) {
				var ifrm = $(this)[0];
				var iframeDocument = ifrm.contentDocument || ifrm.contentWindow.document;
				iframeDocument.body.style.backgroundColor = backgroundColor;
				iframeDocument.body.style.color = fontColor;
			});
		});
	}

	$('.table-email-list .custom-checkbox').click(function (e) {
		e.stopPropagation();
	});

	function checkValidFloat(e, value, min = Number.MIN_SAFE_INTEGER, max = Number.MAX_SAFE_INTEGER) {

		let position_caret = e.target.selectionStart;
		let is_valid_point = true;

		if (value.indexOf('.') >= 0 || value == '-') {
			is_valid_point = false;
		}

		var split_value = value.split('.');
		var digit_number = split_value[0];
		var digit_float = split_value.length > 1 ? split_value[1] : '';

		if (digit_number.length > 14 || digit_float.length > 9)
			return false;

		try {
			value = parseFloat(value);
		} catch (error) { value = 0; }

		try {
			min = parseFloat(min);
		}
		catch (error) { min = Number.MIN_SAFE_INTEGER; }

		try {
			max = parseFloat(max);
		}
		catch (error) { max = Number.MAX_SAFE_INTEGER; }

		if (e.which == 13) {
			return true;
		}
		else if ((e.which > 47 && e.which < 58) || (e.which == 45 && position_caret == 0) ||
			(e.which == 46 && position_caret > 0 && is_valid_point == true)) {

			try {
				let new_value = e.key;
				value = (value ? value.toString() : '');
				let start = value.substring(0, position_caret);
				let end = value.substring(position_caret);
				value = (start.length > 0 ? start : '') + new_value + (end.length > 0 ? end : '');

				value = parseFloat(value);
			}
			catch (error) { }

			if (min > value || max < value) {
				return false;
			}
			else {
				return true;
			}
		}
		else {
			return false;
		}
	}

	function checkValidInteger(e, value, min = Number.MIN_SAFE_INTEGER, max = Number.MAX_SAFE_INTEGER) {

		let position_caret = e.target.selectionStart;

		if (value.toString().length > 14) {
			return false;
		}

		try {
			value = parseInt(value);
		} catch (error) { value = 0; }

		try {
			min = parseInt(min);
		}
		catch (error) { min = Number.MIN_SAFE_INTEGER; }

		try {
			max = parseInt(max);
		}
		catch (error) { max = Number.MAX_SAFE_INTEGER; }

		if (e.which == 13) {
			return true;
		}
		else if ((e.which > 47 && e.which < 58) || (e.which == 45 && position_caret == 0)) {
			try {
				let new_value = e.key;
				value = (value ? value.toString() : '');
				let start = value.substring(0, position_caret);
				let end = value.substring(position_caret);
				value = (start.length > 0 ? start : '') + new_value + (end.length > 0 ? end : '');

				value = parseInt(value);
			}
			catch (error) { }

			if (min > value || max < value) {
				return false;
			}
			else {
				return true;
			}
		}
		else {
			return false;
		}
	}

})(jQuery);