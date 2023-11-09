import Image from "next/image"
import Logo from '../../public/logo.svg'
import { Open_Sans } from "next/font/google"
import  Home from '../../public/home.svg'
import Chart from '../../public/chart.svg'
import Profile from '../../public/profile.svg'
import Stethoscope from '../../public/stethoscope.svg'
import Heart from '../../public/heart.svg'
import Calendar from '../../public/calendar.svg'
import MenuItem from "../components/MenuItem"
import LogoutButton from "../components/LogoutButton"

const openSans = Open_Sans({ subsets: ['latin'] })

export default function DashboardLayout({
	children,
  }: {
	children: React.ReactNode
  }) {
	return (
		<div className='flex'>
			<aside className='flex flex-col justify-between w-72 border-r border-[#EFEFEF h-[100vh] p-6'>
				<div className='flex flex-col gap-11'>
					<div className='flex gap-3 items-center'>
						<Image src={Logo} alt='Logotipo' width={36} height={36} className="m-3"/>
						<span className='text-[#09090A] max-w-[7.2rem] text-base'>Portal do terapeuta</span>
					</div>
					<div className='flex flex-col gap-6 '>
						<MenuItem icon={Home} text='Página inicial' href='/dashboard'/>
						<MenuItem icon={Chart} text='Métricas' href='/dashboard/metrics'/>
						<MenuItem icon={Profile} text='Pacientes' href='/dashboard/patients'/>
						<MenuItem icon={Stethoscope} text='Terapeutas' href='/dashboard/therapists'/>
						<MenuItem icon={Heart} text='Terapias' href='/dashboard/therapies'/>
						<MenuItem icon={Calendar} text='Agenda' href='/dashboard/calendar'/>
					</div>
				</div>
				<LogoutButton/>
			</aside>
			{children}
		</div>
			
	)
  }