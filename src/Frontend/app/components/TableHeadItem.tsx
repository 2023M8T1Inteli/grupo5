export default function TableHeadItem(props : {children: React.ReactNode, className: string}) {
	return (
		<p className={'text-base font-medium text-[#667085]' +  ' ' + props.className}>{props.children}</p>
	)
}