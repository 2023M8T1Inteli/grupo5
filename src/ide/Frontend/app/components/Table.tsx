import TableHeadItem from "./TableHeadItem";

export default function Table({ headers, children } : {headers: {name: string, spacing: string}[], children: React.ReactNode}) {
    return (
		<div className='rounded-lg border-solid border-2 border-[#EAECF0] overflow-hidden'>
			<div className='bg-[#F9FAFB] w-full px-6 py-3 flex'>
				{headers.map((header, index) => (
					<TableHeadItem key={index} className={`w-${header.spacing}`}>{header.name}</TableHeadItem>
				))}
			</div>
			{children}
		</div>
    );
}
