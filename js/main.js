$(document).ready(function() {
	hideUnselectedMenu(0, null);
	$("#empty").show();
	$("#menu ul li").each(function(i) {
		$(this).click(function() {
					var alreadySelected = $(this).hasClass("selected");
					unselectAllMenu();
					curr = i + 1;
					if (!alreadySelected)
					{
						$(this).addClass("selected");
						$(this).removeClass("unselected");
					}
					else
						curr = 1;
					
					hideUnselectedMenu(500, curr);
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
	
	$(document).keyup(function(e) {
		if (e.keyCode == 27)  // ESCAPE
		{
			unselectAllMenu();
			hideUnselectedMenu(500, 1)
		}
		
		if (e.altKey)
		{
			if (e.keyCode > 48 && e.keyCode < 54)
				$("#menu ul li:nth-child(" + (e.keyCode - 47) + ')').trigger('click');
			
		}
	});
});

function hideUnselectedMenu(t, curr) {
	$( ".inner > div" ).each(function(i) {
			if (i + 1 != curr)
			{
				$(this).slideUp(t, function() {
					if (i + 1 == $( ".inner > div" ).length && curr != null)
					{
							$( ".inner div:nth-child(" + curr + ')' ).slideDown(1500);
							if (curr == 1)
								$("#menu ul li").removeClass("unselected");
					}
				});
			}
			else
				$( ".inner div:nth-child(" + curr + ')' ).slideDown(1500);
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