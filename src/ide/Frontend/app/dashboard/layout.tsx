import Sidebar from "../components/Sidebar";

export default function DashboardLayout({ children }: { children: React.ReactNode }) {
	return (
	  <div className='flex w-[100vw]'>
		<Sidebar />
		{children}
	  </div>
	);
}