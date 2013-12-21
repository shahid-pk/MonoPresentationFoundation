open FILE, "data-classes.txt";

%classes;

while ($line = <FILE>) {
	chop $line;
	$properties = "";
	$magicprops = "";
	if (index($line, ":") == -1) {
		$class = $line;
		if (index($line, ";") != -1) {
			$class = substr($line, 0, index($line, ";"));
			$magicprops = substr($line, index($line, ";") + 2);
			print "$class: $magicprops\n";
		}
	} else {
		$class = substr($line, 0, index($line, ":"));
		$properties = substr($line, index($line, ":") + 2);
		if (index($properties, ";") != -1) {
			$magicprops = substr($properties, index($properties, ";") + 2);
			$properties = substr($properties, 0, index($properties, ";"));
		}
	}
	if ($line =~ m/Xaml[^(]+\(/) {
		$parent = $class;
		$parent =~ s/(Xaml[^(]+)\(([^)]+)\).*/\2/;
		$clname = $class;
		$clname =~ s/(Xaml[^(]+)\(([^)]+)\).*/\1/;
		$class = $clname;
	} else {
		$parent = "XamlNode";
	}
	$classes{$class} = { parent => $parent, 
			properties => $properties,
			magicprops => $magicprops };
}

sub get_parent {
	my $x = shift;
	return $classes{$x}->{"parent"};
}
sub get_properties {
	my $x = shift;
	return $classes{$x}->{"properties"};
}
sub get_magic_properties {
	my $x = shift;
	return $classes{$x}->{"magicprops"};
}


sub get_extended_params_list {
	my $x = shift;
	my $params = "";
	while ($x ne "XamlNode") {
		$x = &get_parent($x);
		$params = &get_properties($x) . ", " . $params;
		$params =~ s/, $//;
	}
	$params =~ s/^XamlNodeType tokenType, //;
	return $params;
}

sub has_parent {
	my $x = shift;
	my $parent = shift;
	while ($x ne "object") {
		if ($x eq $parent) {
			return 1;
		}
		$x = &get_parent($x);
	}
	return 0;
}

for $class (keys %classes) {
	print "$class.cs\n";
	open X, ">$class.cs";
	print X "// THIS FILE AUTOMATICALLY GENERATED BY data-classes-builder.pl\n";
	print X "// EDITING IS PROBABLY UNWISE\n";
	print X "namespace System.Windows.Serialization {\n";
	print X "using System;\n";
	print X "using System.Reflection;\n";
	print X "using System.Windows;\n";
	$parent = &get_parent($class);

	$props = &get_properties($class);
	@propl = split /, /, $props;

	$eprops = &get_extended_params_list($class);
	@epropl = split /, /, $eprops;

	$allprops = $eprops;
	$allprops .= ", $props" unless ($props eq "");
	@allpropl = split /, /, $allprops;
	my @allpropl2 = @allpropl;
	for $prop (@allpropl2) {
		$prop =~ s/^\+//;
	}
	$allprops = join ", ", @allpropl2;

	$mprops = &get_magic_properties($class);
	@mpropl = split /, /, $mprops;

	$xamlnodetype = "$class";
	$xamlnodetype =~ s/^Xaml(.+)Node$/\1/;
	$xamlnodetype = "XamlNodeType.$xamlnodetype";

	print X "public class $class : " . $parent . " { \n";

	# member variables
	for $prop (@propl) {
		next if $prop =~ m/^\+/;
		print X "\tprivate $prop;\n";
	}
	for $prop (@mpropl) {
		next if $prop =~ m/^\+/;
		$x = $prop;
		$x =~ s/ / _/;
		print X "\tprivate $x;\n";
	}
	print X "\n\n";

	# constructor
	if ($class eq "XamlNode") {
		print X "\tpublic $class($props)\n";
		print X "\t{\n";
		for $prop (@propl) {
			$propname = substr($prop, index($prop, " ") + 1);
			print X "\t\tthis.$propname = $propname;\n";
		}
		print X "\t}\n";
	} else {
		print X "\tpublic $class($allprops)\n";
		print X "\t\t: this(";
		my @allpropnames;
		push @allpropnames, $xamlnodetype;
		for $prop (@allpropl) {
			push @allpropnames, substr($prop, index($prop, " ") + 1);
		}
		print X (join ", ", @allpropnames);
		print X ")\n\t{}\n\n\n";

		print X "\tinternal $class(XamlNodeType type, $allprops)\n";

		my @epropnamel;
		for $prop (@epropl) {
			push @epropnamel, substr($prop, index($prop, " ") + 1);
		}
		$epropnames = join ", ", @epropnamel;
		print X "\t\t: base(type, $epropnames)\n";
		print X "\t{\n";
		for $prop (@propl) {
			$propname = substr($prop, index($prop, " ") + 1);
			if ($prop =~ m/^\+/) {
				$func = substr($propname, 0, 1);
				$func =~ tr/a-z/A-Z/;
				$func = "set$func" . substr($propname, 1);
				print X "\t\tthis.$func($propname);\n";
			} else {
				print X "\t\tthis.$propname = $propname;\n";
			}
		}
		print X "\t}\n";
	}

	#property accessors
	for $prop (@propl) {
		$proptype = substr($prop, 0, index($prop, " "));
		$fieldname = substr($prop, index($prop, " ") + 1);

		$propname = substr($fieldname, 0, 1);
		$propname =~ tr/a-z/A-Z/;
		$propname .= substr($fieldname, 1);

		if ($proptype =~ m/^\+/) {
			next;
		}

		print X "\n\tpublic $proptype $propname {\n";
		print X "\t\tget { return $fieldname; }\n";
		print X "\t}\n";
	}
	for $prop (@mpropl) {
		$proptype = substr($prop, 0, index($prop, " "));
		$propname = substr($prop, index($prop, " ") + 1);
		$fieldname = "_$propname";

		if ($propname =~ m/^[a-z]/) {
			$access = "internal";
		} else {
			$access = "public";
		}

		print X "\n\t$access $proptype $propname {\n";
		print X "\t\tget { return $fieldname; }\n";
		print X "\t}\n";
		print X "\n\tinternal void set$propname($proptype v) {\n";
		print X "\t\t$fieldname = v;\n";
		print X "\t}\n";
	}
	print X "}\n}\n\n";
	close X;
}
