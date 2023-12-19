'use client'
import Image from "next/image";
import { usePathname } from 'next/navigation'

export default function MenuItem({ icon, text, href }: { icon: any, text: string, href: string }) {
	const pathname = usePathname()
	return (
		<a className='flex gap-4 items-center cursor-pointer hover:scale-105 duration-300' href={href}>
			<Image src={icon} alt='Menu' className="m-3"/>
			<span>{text}</span>
			{pathname === href && <div className='w-1 h-6 bg-[#E7343F] rounded-full'/>}
		</a>
	)
}