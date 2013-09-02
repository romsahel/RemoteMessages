$(document).ready(function() {
	hideUnselectedMenu(0);
	
	$("#menu ul li").each(function(i) {
		$(this).click(function() {
					unselectAllMenu();
					$(this).addClass("selected");
					$(this).removeClass("unselected");
					hideUnselectedMenu(250);
					$( ".inner div:nth-child(" + i + ')' ).fadeIn(1000);
			});
	});
	
	$("#feature-list li").each(function(i) {
		$(this).children().eq(1).slideToggle();
		
		$(this).click(function() {	
				var alreadySelected = $(this).hasClass("selected");
				
				$("#feature-list li").removeClass("selected");
				if (!alreadySelected)
					$(this).addClass("selected");
				$("#feature-list li").each(function() {
					if (!$(this).hasClass("selected"))
						$(this).children().eq(1).slideUp();
				});
				if (!alreadySelected)
					$(this).children().eq(1).slideToggle();
			});
	});
});

function hideUnselectedMenu(i) {
	$( ".inner > div" ).each(function() {
		if (!$(this).hasClass("selected"))
			$(this).fadeOut(i)
			});
	};
	
function unselectAllMenu() {
	$("#menu ul li").addClass("unselected");
	$("#menu ul li").removeClass("selected");
};

function setFooter() {
    if (document.getElementById) {
        var windowHeight=getWindowHeight();
        if (windowHeight>0) {
            var contentHeight=
            document.getElementById('content').offsetHeight;
            var footerElement=document.getElementById('footer');
            var footerHeight=footerElement.offsetHeight;
        if (windowHeight-(contentHeight+footerHeight)>=0) {
            footerElement.style.position='relative';
            footerElement.style.top=
            (windowHeight-(contentHeight+footerHeight))+'px';
        }
        else {
            footerElement.style.position='static';
        }
       }
      }
};
	
function windowHeight() {
  if( typeof( window.innerWidth ) == 'number' ) {
    //Non-IE
    return window.innerHeight;
  } else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {
    //IE 6+ in 'standards compliant mode'
    return document.documentElement.clientHeight;
  } else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {
    //IE 4 compatible
    return document.body.clientHeight;
  }
};